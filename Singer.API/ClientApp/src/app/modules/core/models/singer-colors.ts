/*
   The purpose of this file is to provide the theme colors
   defined in @Angular-project-root/src/sint-gerardus-theme.scss
   inside of Typescript.
*/

/*
   Table of contents:

   1. Primary App Colors
   2. Secondary Accent Colors
   3. Functions
*/

/*
   NameSpace
*/
export namespace SingerColors {
   /*
      =====================
      1. Primary App Colors
      =====================
   */

   /**
      App Primary Color
   */
   export enum Primary {
      a50 = '#e5eefa',
      a100 = '#bfd5f2',
      a200 = '#95b9ea',
      a300 = '#6a9de1',
      a400 = '#4a88da',
      a500 = '#2a73d4' /* Singer Primary Color */,
      a600 = '#256bcf',
      a700 = '#1f60c9',
      a800 = '#1956c3',
      a900 = '#0f43b9',
      A100 = '#e7edff',
      A200 = '#b4c8ff',
      A400 = '#81a2ff',
      A700 = '#6890ff',
   }

   /**
      App Accent Color
   */
   export enum Accent {
      a50 = '#fcfce3',
      a100 = '#f8f9b9',
      a200 = '#f4f58a',
      a300 = '#f0f05b',
      a400 = '#eced37',
      a500 = '#e9ea14' /* Singer Accent Color */,
      a600 = '#e6e712',
      a700 = '#e3e40e',
      a800 = '#dfe10b',
      a900 = '#d9db06',
      A100 = '#ffffff',
      A200 = '#ffffd0',
      A400 = '#feff9d',
      A700 = '#feff84',
   }

   /*
      ==========================
      2. Secondary Accent Colors
      ==========================
   */

   /**
      Aditional Accent Colors Lighter
   */
   export enum SingerAccentColorsLighter {
      lightGreen = '#18a945',
      lightPink = '#ff43a1',
      lightPurple = '#a143ff',
      lightBlue = '#2a73d4',
      lightTbd1 = '#ff43a1',
      lightTbd2 = '#a143ff',
      lightTbd3 = '#18a945',
      lightTbd4 = '#ff43a1',
      lightTbd5 = '#a143ff',
      lightTbd6 = '#18a945',
   }

   /**
      Aditional Accent Colors Darker
   */
   export enum SingerAccentColorsDarker {
      darkGreen = '#18a945',
      darkPink = '#ff43a1',
      darkPurple = '#a143ff',
      darkBlue = '#2a73d4',
      darkTbd1 = '#ff43a1',
      darkTbd2 = '#a143ff',
      darkTbd3 = '#18a945',
      darkTbd4 = '#ff43a1',
      darkTbd5 = '#a143ff',
      darkTbd6 = '#18a945',
   }

   /*
      ============
      3. Functions
      ============
   */

   /**
      Function that returns specified Aditional Lighter Accent Color
      @param number number (0-9) to specify wich color
      @returns color value as string
   */
   export function getSingerAccentColorLighter(number: number): string {
      number = ~~Math.abs(number) % 10; // ~~ operator removes any decimals
      return SingerAccentColorsLighter[
         Object.keys(SingerAccentColorsLighter)[number]
      ];
   }

   /**
      3.2 Function that returns one of the 10 Aditional Lighter Accent Colors
      @returns color value as string
   */
   export function getRandomSingerAccentColorLighter(): string {
      var number = Math.floor(Math.random() * 10 + 1) % 10;
      return SingerAccentColorsLighter[
         Object.keys(SingerAccentColorsLighter)[number]
      ];
   }

   /**
      3.3 Function that returns specified Aditional Darker Accent Color
      @param number number (0-9) to specify wich color
      @returns color value as string
   */
   export function getSingerAccentColorDarker(number: number): string {
      number = ~~Math.abs(number) % 10; // ~~ operator removes any decimals
      return SingerAccentColorsDarker[
         Object.keys(SingerAccentColorsDarker)[number]
      ];
   }

   /**
      3.4 Function that returns one of the 10 Aditional Darker Accent Colors
      @returns color value as string
   */
   export function getRandomSingerAccentColorDarker(): string {
      var number = Math.floor(Math.random() * 10 + 1) % 10;
      return SingerAccentColorsDarker[
         Object.keys(SingerAccentColorsDarker)[number]
      ];
   }
}
