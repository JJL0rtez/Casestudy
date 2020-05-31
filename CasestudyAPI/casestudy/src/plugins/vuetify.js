import Vue from "vue";
import Vuetify from "vuetify/lib";
import colors from "vuetify/lib/util/colors";
Vue.use(Vuetify);
export default new Vuetify({
  theme: {
    themes: {
      light: {
        primary: colors.green.lighten1, // #E53935
        secondary: colors.green.lighten4, // #FFCDD2
        accent: colors.lightGreen.lighten1,
        error: "#FF5252",
        info: "#2196F3",
        success: "#4CAF50",
        warning: colors.lightGreen.lighten3,
      },
    },
  },
});
