import type { SpellShort } from "@/models/spellShort";
import { defineStore } from "pinia";

export const spellsStore = defineStore("spellsData", {
  state: () => {
    const allSpells: SpellShort[] = [];
    return {
      allSpells,
    };
  },
  getters: {
    // upCity: (state) => state.city.toUpperCase(),
  },
});
