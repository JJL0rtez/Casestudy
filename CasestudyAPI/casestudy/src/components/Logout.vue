<template>
  <div align="center" style="margin-top:30vh;font-size:40px;color:green">
    Goodbye!
  </div>
</template>
<script>
export default {
  data() {
    return {};
  },
  created: function() {
    sessionStorage.clear();
  },
};
</script>
