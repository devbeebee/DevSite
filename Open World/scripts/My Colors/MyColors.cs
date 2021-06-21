using System.Collections.Generic;
using UnityEngine;

public static class MyColors
{
    public static Color ColorFromHexidecimal(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color c);
        return c;
    }
    #region Random Color Methods
    public static Color RandomBaseColor => Base.GetRandomColor();
    public static Color RandomBoldColor => Bold.GetRandomColor();
    public static Color RandomDarkColor => Dark.GetRandomColor();
    public static Color RandomLightColor => Light.GetRandomColor();
    public static Color RandomPastelColor => Pastel.GetRandomColor();
    public static Color RandomZestyColor => Zesty.GetRandomColor();
    public static Color GetRandomColor()
    {
        List<Color> Colours = new List<Color>();
        Colours.AddRange(Base.GetColors());
        Colours.AddRange(Bold.GetColors());
        Colours.AddRange(Pastel.GetColors());
        Colours.AddRange(Dark.GetColors());
        Colours.AddRange(Light.GetColors());
        Colours.AddRange(Zesty.GetColors());
        return Colours.PickRandom();
    }
    #endregion
    #region Color Classes
    public static class Base
    {
        public static List<Color> GetColors()
        {
            List<Color> Colours = new List<Color>
            {
                Black,
                Red,
                Green,
                Yellow,
                Blue,
                Cyan,
                Purple,
                White,
                Orange,
                Gray,
                Silver,
                Darkgray,
                Violet
            };
            return Colours;
        }
        public static Color GetRandomColor()
        {
            List<Color> Colours = new List<Color>
            {
                Black,
                Red,
                Green,
                Yellow,
                Blue,
                Cyan,
                Purple,
                White,
                Orange,
                Gray,
                Silver,
                Darkgray,
                Violet
            };
            return Colours.PickRandom();
        }
        private readonly static Color black = new Color(0.00f, 0.00f, 0.00f, 1.00f);
        private readonly static Color red = new Color(1.00f, 0.00f, 0.00f, 1.00f);
        private readonly static Color green = new Color(0.00f, 1.00f, 0.00f, 1.00f);
        private readonly static Color yellow = new Color(1.00f, 1.00f, 0.00f, 1.00f);
        private readonly static Color blue = new Color(0.00f, 0.00f, 1.00f, 1.00f);
        private readonly static Color cyan = new Color(0.00f, 1.00f, 1.00f, 1.00f);
        private readonly static Color purple = new Color(1.00f, 0.00f, 1.00f, 1.00f);
        private readonly static Color white = new Color(1.00f, 1.00f, 1.00f, 1.00f);
        private readonly static Color orange = new Color(1.00f, 0.50f, 0.00f, 1.00f);
        private readonly static Color gray = new Color(0.50f, 0.50f, 0.50f, 1.00f);
        private readonly static Color silver = new Color(0.80f, 0.80f, 0.80f, 1.00f);
        private readonly static Color darkgray = new Color(0.35f, 0.35f, 0.35f, 1.00f);
        private readonly static Color violet = new Color(0.50f, 0.00f, 1.00f, 1.00f);

        public static Color Black => black;
        public static Color Red => red;
        public static Color Green => green;
        public static Color Yellow => yellow;
        public static Color Blue => blue;
        public static Color Cyan => cyan;
        public static Color Purple => purple;
        public static Color White => white;
        public static Color Orange => orange;
        public static Color Gray => gray;
        public static Color Silver => silver;
        public static Color Darkgray => darkgray;
        public static Color Violet => violet;
    }
    public static class Pastel
    {
        public static List<Color> GetColors()
        {
            List<Color> Colours = new List<Color>
            {
                red_chestnut,
                green_meadow,
                blue_steel,
                yellow_celery ,
                orange_twine ,
                purple_amethyst,
                cyan_fountain
            };
            return Colours;
        }
        public static Color GetRandomColor()
        {
            List<Color> Colours = new List<Color>
            {
                red_chestnut,
                green_meadow,
                blue_steel,
                yellow_celery ,
                orange_twine ,
                purple_amethyst,
                cyan_fountain
            };
            return Colours.PickRandom();
        }
        private readonly static Color red_chestnut = new Color(0.72f, 0.32f, 0.32f, 1.00f);
        private readonly static Color green_meadow = new Color(0.32f, 0.72f, 0.42f, 1.00f);
        private readonly static Color blue_steel = new Color(0.32f, 0.42f, 0.72f, 1.00f);
        private readonly static Color yellow_celery = new Color(0.72f, 0.72f, 0.32f, 1.00f);
        private readonly static Color orange_twine = new Color(0.72f, 0.56f, 0.32f, 1.00f);
        private readonly static Color purple_amethyst = new Color(0.72f, 0.32f, 0.72f, 1.00f);
        private readonly static Color cyan_fountain = new Color(0.32f, 0.72f, 0.56f, 1.00f);

        public static Color Red_chestnut => red_chestnut;
        public static Color Green_meadow => green_meadow;
        public static Color Blue_steel => blue_steel;
        public static Color Yellow_celery => yellow_celery;
        public static Color Orange_twine => orange_twine;
        public static Color Purple_amethyst => purple_amethyst;
        public static Color Cyan_fountain => cyan_fountain;
    }
    public static class Dark
    {
        public static List<Color> GetColors()
        {
            List<Color> Colours = new List<Color>
            {
               darkred_maroon,
               darkgreen_laurel,
               darkblue_navy,
               darkyellow_olive,
               darkorange_cinnamon,
               darkpurple_indigo,
               darkcyan_deapsea
            };
            return Colours;
        }
        public static Color GetRandomColor()
        {
            List<Color> Colours = new List<Color>
            {
                darkred_maroon,
                darkgreen_laurel,
                darkblue_navy ,
                darkyellow_olive,
                darkorange_cinnamon ,
                darkpurple_indigo ,
                darkcyan_deapsea
            };
            return Colours.PickRandom();
        }
        private readonly static Color darkred_maroon = new Color(0.50f, 0.00f, 0.00f, 1.00f);
        private readonly static Color darkgreen_laurel = new Color(0.00f, 0.50f, 0.00f, 1.00f);
        private readonly static Color darkblue_navy = new Color(0.00f, 0.00f, 0.50f, 1.00f);
        private readonly static Color darkyellow_olive = new Color(0.50f, 0.50f, 0.00f, 1.00f);
        private readonly static Color darkorange_cinnamon = new Color(0.50f, 0.25f, 0.00f, 1.00f);
        private readonly static Color darkpurple_indigo = new Color(0.25f, 0.00f, 0.50f, 1.00f);
        private readonly static Color darkcyan_deapsea = new Color(0.00f, 0.50f, 0.50f, 1.00f);

        public static Color Darkred_maroon => darkred_maroon;
        public static Color Darkgreen_laurel => darkgreen_laurel;
        public static Color Darkblue_navy => darkblue_navy;
        public static Color Darkyellow_olive => darkyellow_olive;
        public static Color Darkorange_cinnamon => darkorange_cinnamon;
        public static Color Darkpurple_indigo => darkpurple_indigo;
        public static Color Darkcyan_deapsea => darkcyan_deapsea;
    }
    public static class Light
    {
        public static List<Color> GetColors()
        {
            List<Color> Colours = new List<Color>
            {
                red_tangerine ,
                green_mint ,
                blue_malibu ,
                yellow_dolly ,
                orange_macncheese ,
                purple_blush ,
                cyan_anakiwa
            };
            return Colours;
        }
        public static Color GetRandomColor()
        {
            List<Color> Colours = new List<Color>
            {
                red_tangerine ,
                green_mint ,
                blue_malibu ,
                yellow_dolly ,
                orange_macncheese ,
                purple_blush ,
                cyan_anakiwa
            };
            return Colours.PickRandom();
        }
        private readonly static Color red_tangerine = new Color(1.00f, 0.50f, 0.50f, 1.00f);
        private readonly static Color green_mint = new Color(0.50f, 1.00f, 0.50f, 1.00f);
        private readonly static Color blue_malibu = new Color(0.50f, 0.50f, 1.00f, 1.00f);
        private readonly static Color yellow_dolly = new Color(1.00f, 1.00f, 0.50f, 1.00f);
        private readonly static Color orange_macncheese = new Color(1.00f, 0.75f, 0.50f, 1.00f);
        private readonly static Color purple_blush = new Color(1.00f, 0.50f, 1.00f, 1.00f);
        private readonly static Color cyan_anakiwa = new Color(0.50f, 1.00f, 1.00f, 1.00f);

        public static Color Red_tangerine => red_tangerine;
        public static Color Green_mint => green_mint;
        public static Color Blue_malibu => blue_malibu;
        public static Color Yellow_dolly => yellow_dolly;
        public static Color Orange_macncheese => orange_macncheese;
        public static Color Purple_blush => purple_blush;
        public static Color Cyan_anakiwa => cyan_anakiwa;
    }
    public static class Bold
    {
        public static List<Color> GetColors()
        {
            List<Color> Colours = new List<Color>
            {
         red_coral,
         green_earthbound,
         blue_dodger,
         yellow_golden ,
         orange_crusta,
         purple_velectric,
         cyan_aqua,
         pink_strawberry
            };
            return Colours;
        }
        public static Color GetRandomColor()
        {
            List<Color> Colours = new List<Color>
            {
                red_coral,
                green_earthbound,
                blue_dodger,
                yellow_golden,
                orange_crusta,
                purple_velectric,
                cyan_aqua,
                pink_strawberry
            };
            return Colours.PickRandom();
        }
        private readonly static Color red_coral = new Color(1.00f, 0.25f, 0.25f, 1.00f);
        private readonly static Color green_earthbound = new Color(0.25f, 1.00f, 0.50f, 1.00f);
        private readonly static Color blue_dodger = new Color(0.25f, 0.50f, 1.00f, 1.00f);
        private readonly static Color yellow_golden = new Color(1.00f, 1.00f, 0.25f, 1.00f);
        private readonly static Color orange_crusta = new Color(1.00f, 0.50f, 0.25f, 1.00f);
        private readonly static Color purple_velectric = new Color(0.50f, 0.25f, 1.00f, 1.00f);
        private readonly static Color cyan_aqua = new Color(0.25f, 1.00f, 1.00f, 1.00f);
        private readonly static Color pink_strawberry = new Color(1.00f, 0.25f, 0.50f, 1.00f);

        public static Color Red_coral => red_coral;
        public static Color Green_earthbound => green_earthbound;
        public static Color Blue_dodger => blue_dodger;
        public static Color Yellow_golden => yellow_golden;
        public static Color Orange_crusta => orange_crusta;
        public static Color Purple_velectric => purple_velectric;
        public static Color Cyan_aqua => cyan_aqua;
        public static Color Pink_strawberry => pink_strawberry;
    }
    public static class Zesty
    {
        public static List<Color> GetColors()
        {
            List<Color> Colours = new List<Color>
            {
                zestyred_bittersweet,
                zestygreen_screamin,
                zestyblue_maya,
                zestyyellow_lemon,
                zestyorange_tainoi,
                zestypurple_heliotrope ,
                zestycyan_aquamarine ,
                zestypink_neon
            };
            return Colours;
        }
        public static Color GetRandomColor()
        {
            List<Color> Colours = new List<Color>
            {
                zestyred_bittersweet,
                zestygreen_screamin,
                zestyblue_maya,
                zestyyellow_lemon,
                zestyorange_tainoi,
                zestypurple_heliotrope ,
                zestycyan_aquamarine ,
                zestypink_neon
            };
            return Colours.PickRandom();
        }
        private readonly static Color zestyred_bittersweet = MyColors.ColorFromHexidecimal("#ff5b5b");
        private readonly static Color zestygreen_screamin = MyColors.ColorFromHexidecimal("#91ff5b");
        private readonly static Color zestyblue_maya = MyColors.ColorFromHexidecimal("#5bc3ff");
        private readonly static Color zestyyellow_lemon = MyColors.ColorFromHexidecimal("#fffd5b");
        private readonly static Color zestyorange_tainoi = MyColors.ColorFromHexidecimal("#ffbf5b");
        private readonly static Color zestypurple_heliotrope = MyColors.ColorFromHexidecimal("#da5bff");
        private readonly static Color zestycyan_aquamarine = MyColors.ColorFromHexidecimal("#5bffda");
        private readonly static Color zestypink_neon = MyColors.ColorFromHexidecimal("#ff5bc3");

        public static Color Zestyred_bittersweet => zestyred_bittersweet;
        public static Color Zestygreen_screamin => zestygreen_screamin;
        public static Color Zestyblue_maya => zestyblue_maya;
        public static Color Zestyyellow_lemon => zestyyellow_lemon;
        public static Color Zestyorange_tainoi => zestyorange_tainoi;
        public static Color Zestypurple_heliotrope => zestypurple_heliotrope;
        public static Color Zestycyan_aquamarine => zestycyan_aquamarine;
        public static Color Zestypink_neon => zestypink_neon;
    }
    #endregion
}