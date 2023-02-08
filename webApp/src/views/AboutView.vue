<script lang="ts">
import { spellsStore } from "@/stores/spells";
import axios from "axios";

export default {
  data() {
    return {
      spellsData: spellsStore(),
      errorMessage: "",
    };
  },
  beforeMount() {
    if (this.spellsData.allSpells.length == 0) {
      const uri: string = "https://localhost:7082/DndApi/GetAllSpells";

      axios
        .get(uri)
        .then((response) => {
          this.spellsData.allSpells = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    }
  },
  // computed: {
  //   uri(): string {
  //     var uriString =
  //       "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/" +
  //       this.weatherData.city +
  //       "?unitGroup=metric&include=current&key=" +
  //       this.weatherData.apiKey +
  //       "&contentType=json";
  //     return uriString;
  //   },
  // },
  // methods: {
  //   async onClick() {
  //     await axios
  //       .get(this.uri)
  //       .then((response) => {
  //         console.log(response);
  //         this.result = response.data;
  //         this.current = response.data.currentConditions;
  //         this.errorMessage = "";
  //       })
  //       .catch((err) => {
  //         console.log(err);
  //         this.errorMessage = err;
  //         this.result = new WeatherObject();
  //       });
  //   },
  // },
};
</script>

<template>
  <div class="about">
    <h1>lalala {{ spellsData.allSpells[2]?.name }} bumparara</h1>
    <!-- <h1>This is an about page to test in {{ spellsData.allSpells }}</h1> -->
  </div>
</template>

<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
