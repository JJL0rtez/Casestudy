<template>
  <v-container>
    <v-row justify="center">
      <v-col class="display-1 text-center" style="color:#2FB300">Cart Contents</v-col>
    </v-row>
    <v-row>
      <v-col justify="center" align="center">
        <v-img
          :src="require('../assets/cart.png')"
          style="height:10vh;width:14vh;"
          aspect-ratio="1"
          height="25px"
        />
      </v-col>
    </v-row>
    <v-row style="margin:2vw;">{{ status }}</v-row>
    <div v-if="this.status !== 'cart cleared'">
      <v-simple-table
        style="color:#2FB300;width:97%;float:center;"
        dense
        height="240px"
        border-bottom="solid"
      >
        <tbody>
          <tr style="fontWeight:bold;fontSize:14">
            <td style="width:10%">#</td>
            <td>Name</td>
            <td>Qty</td>
            <td>Price</td>
            <td style="text-align: left;">Ext</td>
          </tr>

          <tr v-for="it in this.cart" :key="it.id">
            <td>{{ it.id }}</td>
            <td>{{ it.item.productName }}</td>
            <td>{{ it.qty }}</td>
            <td>{{ it.item.costPrice | currency }}</td>
            <td>{{ (it.item.costPrice * it.qty) | currency }}</td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>_____________</td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>sub total:</td>
            <td>{{ subTotal | currency }}</td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>tax:</td>
            <td>{{ taxTotal | currency }}</td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>cart total:</td>
            <td style="fontWeight:bold;">{{ cartTotal | currency }}</td>
          </tr>
        </tbody>
      </v-simple-table>

      <v-btn
        style="float:right;margin:25px"
        medium
        outlined
        color="#2FB300"
        @click="clearCart"
      >Clear Cart</v-btn>
      <v-btn style="float:left;margin:25px" medium outlined color="#2FB300" @click="addCart">Save</v-btn>
    </div>
  </v-container>
</template>
<script>
import fetcher from "../mixins/fetcher";
export default {
  name: "CartDetails",
  data() {
    return {
      taxTotal: 0,
      subTotal: 0,
      cartTotal: 0,
      cart: [],
      status: ""
    };
  },
  beforeMount: function() {
    if (sessionStorage.getItem("cart")) {
      this.cart = JSON.parse(sessionStorage.getItem("cart"));
      this.cart.map(cartItem => {
        this.subTotal += cartItem.item.costPrice * cartItem.qty;
        this.taxTotal +=
          (cartItem.item.msrp - cartItem.item.costPrice) * cartItem.qty;
        this.cartTotal += cartItem.item.msrp * cartItem.qty;
      });
    } else {
      this.cart = [];
    }
  },
  mixins: [fetcher],
  methods: {
    clearCart: function() {
      sessionStorage.removeItem("cart");
      this.cart = [];
      this.status = "cart cleared";
    },
    addCart: async function() {
      let customer = JSON.parse(sessionStorage.getItem("user"));
      let cart = JSON.parse(sessionStorage.getItem("cart"));

      try {
        this.status = "sending cart info to server";
        console.log(customer.email);
        console.log(cart);
        let cartHelper = { email: customer.email, selections: cart };
        let payload = await this.$_postdata("order", cartHelper); // in mixin

        if (payload.indexOf("not") > 0) {
          //console.log(payload.);
          this.status = payload;
        } else {
          this.clearTray();
          this.status = payload;
        }
      } catch (err) {
        console.log(err);
        this.status = `Error add cart: ${err}`;
      }
    }
  }
};
</script>
