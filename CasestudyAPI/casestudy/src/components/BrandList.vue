<template>
  <v-container fluid>
    <v-row justify="center" class="text-center display-2" style="margin-top:.5em;">Brands</v-row>
    <v-row justify="center">
      <v-col class="title text-center" style="color:red">{{ status }}</v-col>
    </v-row>
    <v-row justify="center">
      <v-col class="text-left display-1">
        <v-select
          :items="brands"
          item-text="name"
          style="max-height: 50%;"
          item-value="id"
          @input="changeProduct"
          v-model="selectedid"
        ></v-select>
      </v-col>
    </v-row>
    <div v-if="products.length > 0">
      <v-row justify="center" class="text-center headline">Products</v-row>
      <v-row justify="center" style="margin-top:1vh;">
        <v-col class="text-left display-2">
          <v-list style="max-height: 60vh;" class="overflow-y-auto">
            <v-list-item-group>
              <v-list-item
                @click="popDialog(item)"
                v-for="(item, i) in products"
                :key="i"
                style="border:solid;"
              >
                <v-col style="width:25%;">
                  <v-img
                    :src="require('../assets/' + item.graphicName)"
                    class="my-3"
                    style="height:10vh;width:10vh;"
                    aspect-ratio="1"
                  />
                </v-col>
                <v-col style="width:75%;">
                  <v-list-item-content>
                    <v-list-item-title class="title" v-text="item.productName">></v-list-item-title>
                  </v-list-item-content>
                </v-col>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-col>
      </v-row>
    </div>
    <v-dialog v-model="dialog" v-if="selectedProduct" justify="center" align="center">
      <v-card>
        <div style="margin:3vw;padding-left:1vw;padding-right:1vw;">
          <v-row>
            <v-spacer></v-spacer>
            <v-btn text @click="dialog = false" style="font-size:XX-large;margin:1vw;">X</v-btn>
          </v-row>
          <v-row>
            <v-col
              class="title"
              justify="center"
              align="center"
              style="margin-left:3vw;margin-right:3vw;"
            >{{ selectedProduct.productName }}</v-col>
            <v-col v-if="selectedProduct.graphicName">
              <v-img
                :src="require('../assets/' + selectedProduct.graphicName)"
                height="15vh"
                width="15vh"
                contain
                aspect-ratio="1"
              />
            </v-col>
          </v-row>
          <v-row justify="center">
            <v-col
              style="margin:3vwpadding-left:5vw;padding-right:5vw;font-weight:bold;font-size:15px;"
            >MSRP: {{ selectedProduct.msrp | currency }}</v-col>
          </v-row>
        </div>
        <div style="margin:3vw;padding-left:1vw;padding-right:1vw;">
          <v-row>
            <v-col
              style="margin:3vwpadding-left:5vw;padding-right:5vw;font-weight:bold;"
            >Description:</v-col>
          </v-row>
          <v-row>
            <v-col>{{ selectedProduct.description }}</v-col>
          </v-row>
          <v-row style="margin-left:1vw;">
            <v-col>Qty:</v-col>
            <v-col>
              <input
                type="number"
                maxlength="3"
                placeholder="enter qty"
                size="3"
                style="width: 15vw;border-bottom:solid;text-align:right"
                v-model="qty"
              />
            </v-col>
            <v-col cols="7"></v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-row justify="center" align="center" style="margin-bottom:2vh;margin-left:3vw;">
                <v-col>
                  <v-btn medium outlined color="default" @click="addToCart">Add To Cart</v-btn>
                </v-col>
                <v-col>
                  <v-btn medium outlined color="default" @click="viewCart">View Cart</v-btn>
                </v-col>
              </v-row>
            </v-col>
          </v-row>
          <v-row>
            <v-col></v-col>
          </v-row>
        </div>
        <v-row justify="center" align="center" style="padding-bottom:5vh;">
          {{
          this.dialogStatus
          }}
        </v-row>
      </v-card>
    </v-dialog>

    <v-footer absolute class="headline">
      <v-col class="text-center" cols="12">&copy;{{ new Date().getFullYear() }} — INFO3067</v-col>
    </v-footer>
  </v-container>
</template>
<script>
import fetcher from "../mixins/fetcher";
export default {
  name: "ProductList",
  data() {
    return {
      brands: [],
      status: {},
      selectedid: 0,
      products: [],
      dialog: false,
      selectedProduct: {},
      dialogStatus: "",
      qty: 0,
      cart: []
    };
  },
  mixins: [fetcher],
  mounted: async function() {
    try {
      this.status = "fetching brands from server...";
      this.brands = await this.$_getdata("brand"); // $_getdata is in mixin
      this.status = `loaded ${this.brands.length + 1} brands`;
      this.brands.unshift({ name: "Current Brands", id: 0 });
      //console.log(this.brands);
    } catch (err) {
      console.log(err);
      //console.log(this.brands);
      this.status = `Error has occured: ${err.message}`;
    }
    if (sessionStorage.getItem("cart")) {
      this.cart = await JSON.parse(sessionStorage.getItem("cart"));
    }
  },
  methods: {
    changeProduct: async function(brandid) {
      if (brandid > 0) {
        // don't use arrow function here
        try {
          this.status = `fetching items for ${brandid}...`;
          this.products = await this.$_getdata(`product/${brandid}`);
          this.status = `found ${this.products.length} items`;
        } catch (err) {
          console.log(err);
          this.status = `Error has occured: ${err.message}`;
        }
      }
    },
    popDialog: function(item) {
      this.dialogStatus = "";
      this.dialog = !this.dialog;
      this.selectedProduct = item;
    },
    addToCart: function() {
      const index = this.cart.findIndex(
        // is item already on the cart
        item => item.id === this.selectedProduct.id
      );
      if (this.qty !== "0") {
        index === -1
          ? this.cart.push({
              id: this.selectedProduct.id,
              qty: parseInt(this.qty),
              item: this.selectedProduct
            }) // add
          : (this.cart[index] = {
              // replace
              id: this.selectedProduct.id,
              qty: parseInt(this.qty),
              item: this.selectedProduct
            });
        this.dialogStatus = `${this.qty} item(s) added`;
      } else {
        index === -1 ? null : this.cart.splice(index, 1); // remove
        this.dialogStatus = `item(s) removed`;
      }
      this.qty = 0;
      sessionStorage.setItem("cart", JSON.stringify(this.cart));
    },
    viewCart: function() {
      this.$router.push({
        name: "cart"
      });
    }
  }
};
</script>
