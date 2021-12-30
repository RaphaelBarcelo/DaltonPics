using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;


namespace Daltonpics.ViewModels
{
    public class BaseRGBViewModel : BaseViewModel
    {



        private readonly List<ColorName> colorsList;    // Liste contenant les noms de couleurs et leur valeur RGB



        private string _colorDisplayName;

        public string ColorDisplayName
        {
            get { return _colorDisplayName; }
            set { SetProperty(ref _colorDisplayName, value); }

        }

        private string _complementaryColorDisplayName;

        public string ComplementaryColorDisplayName
        {
            get { return _complementaryColorDisplayName; }
            set { SetProperty(ref _complementaryColorDisplayName, value); }
        }


        private string _htmlColor;

        public string HTMLColor
        {
            get { return _htmlColor; }
            set
            {
                SetProperty(ref _htmlColor, value);
            }
        }

        private SolidColorBrush _htmlBrush;

        public SolidColorBrush HTMLBrush
        {
            get { return _htmlBrush; }
            set { SetProperty(ref _htmlBrush, value); }
        }


        private string _complementaryColor;

        public string Complementarycolor
        {
            get { return _complementaryColor; }
            set
            {
                SetProperty(ref _complementaryColor, value);
            }
        }


        private SolidColorBrush _complementaryBrush;

        public SolidColorBrush ComplementaryBrush
        {
            get { return _complementaryBrush; }
            set { SetProperty(ref _complementaryBrush, value); }
        }



        private string _leftComplementaryColorName;

        public string LeftComplementaryColorName
        {
            get { return _leftComplementaryColorName; }
            set { SetProperty(ref _leftComplementaryColorName, value); }
        }


        private string _leftComplementaryColor;

        public string LeftComplementaryColor
        {
            get { return _leftComplementaryColor; }
            set { SetProperty(ref _leftComplementaryColor, value); }
        }

        private SolidColorBrush _leftComplementaryBrush;

        public SolidColorBrush LeftComplementaryBrush
        {
            get { return _leftComplementaryBrush; }
            set { SetProperty(ref _leftComplementaryBrush, value); }
        }



        private string _rightComplementaryColorName;

        public string RightComplementaryColorName
        {
            get { return _rightComplementaryColorName; }
            set { SetProperty(ref _rightComplementaryColorName, value); }
        }


        private SolidColorBrush _rightComplementaryBrush;

        public SolidColorBrush RightComplementaryBrush
        {
            get { return _rightComplementaryBrush; }
            set { SetProperty(ref _rightComplementaryBrush, value); }
        }

        private string _rightComplementaryColor;

        public string RightComplementaryColor
        {
            get { return _rightComplementaryColor; }
            set { SetProperty(ref _rightComplementaryColor, value); }
        }



        #region RGB Colors

        private byte _red;

        public byte Red
        {
            get { return _red; }
            set { SetProperty(ref _red, value); }
        }
        private byte _green;

        public byte Green
        {
            get { return _green; }
            set { SetProperty(ref _green, value); }
        }
        private byte _blue;

        public byte Blue
        {
            get { return _blue; }
            set { SetProperty(ref _blue, value); }
        }
        #endregion


        public BaseRGBViewModel()
        {

            Title = "Base RGB";

            colorsList = FillColorsList();

            Red = 0x2b;
            Green = 0xc1;
            Blue = 0xfe;

            SKColor color = new SKColor(Red, Green, Blue);

            CalculateBaseColors(color);
            CalculateComplementaryColors(color);
        }

        /// <summary>
        /// Calculates the base color
        /// </summary>
        /// <param name="pixel"> Pixel retreived </param>
        public void CalculateBaseColors(SKColor pixel)
        {
            Red = pixel.Red;
            Green = pixel.Green;
            Blue = pixel.Blue;
            HTMLColor = String.Format("#{0:X2}{1:X2}{2:X2}", pixel.Red, pixel.Green, pixel.Blue);
            HTMLBrush = new SolidColorBrush(Xamarin.Forms.Color.FromRgb(pixel.Red, pixel.Green, pixel.Blue));
            // Get base color name
            ColorDisplayName = GetColorNameFromRgb(pixel.Red, pixel.Green, pixel.Blue);
        }

        /// <summary>
        /// Calculates de complementary colors
        /// </summary>
        /// <param name="pixel"> Original pixel </param>
        public void CalculateComplementaryColors(SKColor pixel)
        {

            byte red, green, blue;

            // !st complement of the components of the base color
            // to obtain the complemenrary color
            red = (byte)(pixel.Red ^ 0xFF);
            green = (byte)(pixel.Green ^ 0xff);
            blue = (byte)(pixel.Blue ^ 0xff);



            // Exe value string
            Complementarycolor = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);
            ComplementaryBrush = new SolidColorBrush(Xamarin.Forms.Color.FromRgb(red, green, blue));

            // Get color display name
            ComplementaryColorDisplayName = GetColorNameFromRgb(red, green, blue);


            // Left and right Complementary colors

            // Create SKColor from red green blue 
            SKColor color = new SKColor(red, green, blue);

            // Retreive HSL from SKColor
            color.ToHsl(out float h, out float s, out float l);

            // We move Hue to get colors left or right

            // Left complementary color - 20º left
            SKColor leftColor = SKColor.FromHsl(h - 30, s, l);
            LeftComplementaryColor = String.Format("#{0:X2}{1:X2}{2:X2}", (byte)leftColor.Red, (byte)leftColor.Green, (byte)leftColor.Blue);
            LeftComplementaryBrush = new SolidColorBrush(Xamarin.Forms.Color.FromRgb((int)leftColor.Red, (int)leftColor.Green, (int)leftColor.Blue));
            LeftComplementaryColorName = GetColorNameFromRgb((int)leftColor.Red, (int)leftColor.Green, (int)leftColor.Blue);

            // Right complementary color - 20º right
            SKColor rightColor = SKColor.FromHsl(h + 30, s, l);
            RightComplementaryColor = String.Format("#{0:X2}{1:X2}{2:X2}", (byte)rightColor.Red, (byte)rightColor.Green, (byte)rightColor.Blue);
            RightComplementaryBrush = new SolidColorBrush(Xamarin.Forms.Color.FromRgb(rightColor.Red, (int)rightColor.Green, (int)rightColor.Blue));
            RightComplementaryColorName = GetColorNameFromRgb((int)rightColor.Red, (int)rightColor.Green, (int)rightColor.Blue);

        }


        /// <summary>
        /// Fill the reference color list
        /// </summary>
        /// <returns></returns>
        private List<ColorName> FillColorsList()
        {
            List<ColorName> couleurs;
            #region Couleurs en français
            couleurs = new List<ColorName>
            {
                new ColorName("Abricot", 0xE6, 0x7E, 0x30),
                new ColorName("Acajou", 0x88, 0x42, 0x1D),
                new ColorName("Aigue-marine", 0x79, 0xF8, 0xF8),
                new ColorName("Alezan (chevaux)", 0xA7, 0x67, 0x26),
                new ColorName("Amande", 0x82, 0xC4, 0x6C),
                new ColorName("Amarante", 0x91, 0x28, 0x3B),
                new ColorName("Ambre", 0xF0, 0xC3, 0x0),
                new ColorName("Améthyste", 0x88, 0x4D, 0xA7),
                new ColorName("Anthracite", 0x30, 0x30, 0x30),
                new ColorName("Aquilain (chevaux)", 0xAD, 0x4F, 0x9),
                new ColorName("Argent (héraldique)", 0xFE, 0xFE, 0xFE),
                new ColorName("Aubergine", 0x37, 0x0, 0x28),
                new ColorName("Auburn (cheveux)", 0x9D, 0x3E, 0x0C),
                new ColorName("Aurore", 0xFF, 0xCB, 0x60),
                new ColorName("Avocat", 0x56, 0x82, 0x3),
                new ColorName("Azur", 0x0, 0x7F, 0xFF),
                new ColorName("Azur brume", 0xF0, 0xFF, 0xFF),
                new ColorName("Baillet (chevaux, vieilli)", 0xAE, 0x64, 0x2D),
                new ColorName("Basané (teint)", 0x8B, 0x6C, 0x42),
                new ColorName("Beurre", 0xF0, 0xE3, 0x6B),
                new ColorName("Bis", 0x76, 0x6F, 0x64),
                new ColorName("Bisque", 0xFF, 0xE4, 0xC4),
                new ColorName("Bistre", 0x85, 0x6D, 0x4D),
                new ColorName("Bitume (pigment)", 0x4E, 0x3D, 0x28),
                new ColorName("Blanc", 0xFF, 0xFF, 0xFF),
                new ColorName("Blanc cassé", 0xFE, 0xFE, 0xE2),
                new ColorName("Blanc lunaire", 0xF4, 0xFE, 0xFE),
                new ColorName("Blé", 0xE8, 0xD6, 0x30),
                new ColorName("Bleu acier", 0x3A, 0x8E, 0xBA),
                new ColorName("Bleu barbeau ou bleuet", 0x54, 0x72, 0xAE),
                new ColorName("Bleu canard", 0x4, 0x8B, 0x9A),
                new ColorName("Bleu céleste", 0x26, 0xC4, 0xEC),
                new ColorName("Bleu charrette", 0x8E, 0xA2, 0xC6),
                new ColorName("Bleu ciel", 0x77, 0xB5, 0xFE),
                new ColorName("Bleu de cobalt", 0x22, 0x42, 0x7C),
                new ColorName("Bleu de Prusse, de Berlin ou bleu hussard", 0x24, 0x44, 0x5C),
                new ColorName("Bleu électrique", 0x2C, 0x75, 0xFF),
                new ColorName("Bleu givré", 0x80, 0xD0, 0xD0),
                new ColorName("Bleu lavende", 0xE6, 0xE6, 0xFA),
                new ColorName("Bleu marine", 0x3, 0x22, 0x4C),
                new ColorName("Bleu nuit", 0x0F, 0x5, 0x6B),
                new ColorName("Bleu outremer (pigment)", 0x0, 0x47, 0x87),
                new ColorName("Bleu paon", 0x6, 0x77, 0x90),
                new ColorName("Bleu persan", 0x4E, 0x63, 0xCE),
                new ColorName("Bleu pétrole", 0x1D, 0x48, 0x51),
                new ColorName("Bleu roi ou de France", 0x31, 0x8C, 0xE7),
                new ColorName("Bleu turquin", 0x42, 0x5B, 0x8A),
                // Nuances de bleu complémentaires
                new ColorName("Bleu lavande", 0xE6, 0xE6, 0xFA),
                new ColorName("Bleu dragée", 0xDF, 0xF2, 0xFF),
                new ColorName("Bleu poudre", 0xB0, 0xE0, 0xE6),
                new ColorName("Bleu fumée", 0xBB, 0xD2, 0xE1),
                new ColorName("Bleu acier clair", 0xB0, 0xC4, 0xDE),
                new ColorName("Bleu clair", 0xAD, 0xD8, 0xE6),
                new ColorName("Bleu azur clair", 0x87, 0xCE, 0xFA),
                new ColorName("Bleu azur", 0x87, 0xCE, 0xEB),
                new ColorName("Bleu givré", 0x80, 0xD0, 0xD0),
                new ColorName("Bleu pétrole", 0x5F, 0x9E, 0xA0),
                new ColorName("Bleu azur profond", 0x00, 0xBF, 0xFF),
                new ColorName("Bleu ciel", 0x77, 0xB5, 0xFE),
                new ColorName("Bleuet", 0x64, 0x95, 0xED),
                new ColorName("Bleu bleuet foncé", 0x54, 0x72, 0xAE),
                new ColorName("Bleu acier", 0x46, 0x82, 0xB4),
                new ColorName("Bleu toile", 0x1E, 0x90, 0xFF),
                new ColorName("Bleu roi (France)", 0x31, 0x8C, 0xE7),
                new ColorName("Bleu céleste", 0x00, 0x7F, 0xFF),
                new ColorName("Bleu électrique", 0x2C, 0x75, 0xFF),
                new ColorName("Bleu royal", 0x41, 0x69, 0xE1),
                new ColorName("Denim", 0x15, 0x60, 0xBD),
                new ColorName("Bleu", 0x00, 0x00, 0xFF),
                new ColorName("Bleu saphir", 0x01, 0x31, 0xB4),
                new ColorName("Bleu moyen", 0x00, 0x00, 0xCD),
                new ColorName("Bleu outremer", 0x1B, 0x01, 0x9B),
                new ColorName("Bleu foncé", 0x00, 0x00, 0x8B),
                new ColorName("Bleu de minuit", 0x19, 0x19, 0x70),
                new ColorName("Bleu marin", 0x00, 0x00, 0x80),
                new ColorName("Bleu nuit", 0x0F, 0x05, 0x6B),
                new ColorName("Bleu marine", 0x03, 0x22, 0x4C),
                new ColorName("Bleu de cobalt", 0x22, 0x42, 0x7C),
                new ColorName("Bleu de minuit", 0x00, 0x33, 0x66),
                new ColorName("Bleu de Prusse", 0x24, 0x44, 0x5C),
                new ColorName("Bleu pétrole foncé", 0x1D, 0x48, 0x51),
                // Fin couleurs bleue complémentaires


                new ColorName("Blond vénitien (cheveux)", 0xE7, 0xA8, 0x54),
                new ColorName("Blond (cheveux)", 0xE2, 0xBC, 0x74),
                new ColorName("Bouton d'or", 0xFC, 0xDC, 0x12),
                new ColorName("Brique", 0x84, 0x2E, 0x1B),
                new ColorName("Bronze", 0x61, 0x4E, 0x1A),
                new ColorName("Brou de noix", 0x3F, 0x22, 0x4),
                new ColorName("Caca d'oie", 0xCD, 0xCD, 0x0D),
                new ColorName("Cacao", 0x61, 0x4B, 0x3A),
                new ColorName("Cachou (pigments)", 0x2F, 0x1B, 0x0C),
                new ColorName("Cæruleum", 0x35, 0x7A, 0xB7),
                new ColorName("Café", 0x46, 0x2E, 0x1),
                new ColorName("Café au lait", 0x78, 0x5E, 0x2F),
                new ColorName("Cannelle", 0x7E, 0x58, 0x35),
                new ColorName("Capucine", 0xFF, 0x5E, 0x4D),
                new ColorName("Caramel (pigments)", 0x7E, 0x33, 0x0),
                new ColorName("Carmin (pigment)", 0x96, 0x0, 0x18),
                new ColorName("Carotte", 0xF4, 0x66, 0x1B),
                new ColorName("Chamois", 0xD0, 0xC0, 0x7A),
                new ColorName("Chartreuse", 0x7F, 0xFF, 0x0),
                new ColorName("Châtain (cheveux)", 0x8B, 0x6C, 0x42),
                new ColorName("Chaudron", 0x85, 0x53, 0x0F),
                new ColorName("Chocolat", 0x5A, 0x3A, 0x22),
                new ColorName("Cinabre (pigment)", 0xDB, 0x17, 0x2),
                new ColorName("Citrouille", 0xDF, 0x6D, 0x14),
                new ColorName("Coquille d'œuf", 0xFD, 0xE9, 0xE0),
                new ColorName("Corail", 0xE7, 0x3E, 0x1),
                new ColorName("Cramoisi", 0xDC, 0x14, 0x3C),
                new ColorName("Cuisse de nymphe", 0xFE, 0xE7, 0xF0),
                new ColorName("Cuivre", 0xB3, 0x67, 0x0),
                new ColorName("Cyan", 0x2B, 0xFA, 0xFA),
                new ColorName("Écarlate", 0xED, 0x0, 0x0),
                new ColorName("Écru", 0xFE, 0xFE, 0xE0),
                new ColorName("Émeraude (pigment PG18)", 0x0, 0x81, 0x5F),
                new ColorName("Fauve", 0xAD, 0x4F, 0x9),
                new ColorName("Flave", 0xE6, 0xE6, 0x97),
                new ColorName("Fraise", 0xBF, 0x30, 0x30),
                new ColorName("Fraise écrasée", 0xA4, 0x24, 0x24),
                new ColorName("Framboise", 0xC7, 0x2C, 0x48),
                new ColorName("Fuchsia", 0xFD, 0x3F, 0x92),
                new ColorName("Fumée", 0xBB, 0xD2, 0xE1),
                new ColorName("Garance (pigment)", 0xEE, 0x10, 0x10),
                new ColorName("Glauque", 0x64, 0x9B, 0x88),
                new ColorName("Glycine", 0xC9, 0xA0, 0xDC),
                new ColorName("Grège", 0xBB, 0xAE, 0x98),
                new ColorName("Grenadine", 0xE9, 0x38, 0x3F),
                new ColorName("Grenat", 0x6E, 0x0B, 0x14),
                new ColorName("Gris acier", 0xAF, 0xAF, 0xAF),
                new ColorName("Gris de Payne", 0x67, 0x71, 0x79),
                new ColorName("Gris fer", 0x7F, 0x7F, 0x7F),
                new ColorName("Gris perle", 0xCE, 0xCE, 0xCE),
                new ColorName("Gris souris", 0x9E, 0x9E, 0x9E),
                new ColorName("Groseille", 0xCF, 0x0A, 0x1D),
                new ColorName("Gueules (héraldique)", 0xE2, 0x13, 0x13),
                new ColorName("Héliotrope", 0xDF, 0x73, 0xFF),
                new ColorName("Incarnat", 0xFF, 0x6F, 0x7D),
                new ColorName("Indigo", 0x79, 0x1C, 0xF8),
                new ColorName("Indigo (teinture)", 0x2E, 0x0, 0x6C),
                new ColorName("Isabelle", 0xFE, 0xA7, 0x77),
                new ColorName("Jaune canari", 0xE7, 0xF0, 0x0D),
                new ColorName("Jaune citron", 0xF7, 0xFF, 0x3C),
                new ColorName("Jaune d'or", 0xEF, 0xD8, 0x7),
                new ColorName("Jaune de cobalt", 0xFD, 0xEE, 0x0),
                new ColorName("Jaune de Mars (pigment)", 0xEE, 0xD1, 0x53),
                new ColorName("Jaune de Naples (pigment)", 0xFF, 0xF0, 0xBC),
                new ColorName("Jaune impérial", 0xFF, 0xE4, 0x36),
                new ColorName("Jaune mimosa", 0xFE, 0xF8, 0x6C),
                new ColorName("Lapis-lazuli", 0x26, 0x61, 0x9C),
                new ColorName("Lavallière (reliure)", 0x8F, 0x59, 0x22),
                new ColorName("Lavande", 0x96, 0x83, 0xEC),
                new ColorName("Lie de vin", 0xAC, 0x1E, 0x44),
                new ColorName("Lilas", 0xB6, 0x66, 0xD2),
                new ColorName("Lime ou vert citron", 0x9E, 0xFD, 0x38),
                new ColorName("Lin", 0xFA, 0xF0, 0xE6),
                new ColorName("Magenta", 0xFF, 0x0, 0xFF),
                new ColorName("Maïs", 0xFF, 0xDE, 0x75),
                new ColorName("Malachite", 0x1F, 0xA0, 0x55),
                new ColorName("Mandarine", 0xFE, 0xA3, 0x47),
                new ColorName("Marron", 0x58, 0x29, 0x0),
                new ColorName("Mastic", 0xB3, 0xB1, 0x91),
                new ColorName("Mauve", 0xD4, 0x73, 0xD4),
                new ColorName("Menthe", 0x16, 0xB8, 0x4E),
                new ColorName("Moutarde", 0xC7, 0xCF, 0x0),
                new ColorName("Nacarat", 0xFC, 0x5D, 0x5D),
                new ColorName("Nankin", 0xF7, 0xE2, 0x69),
                new ColorName("Noir", 0x00, 0x00, 0x00),
                new ColorName("Noisette", 0x95, 0x56, 0x28),
                new ColorName("Ocre jaune", 0xDF, 0xAF, 0x2C),
                new ColorName("Ocre rouge", 0xDD, 0x98, 0x5C),
                new ColorName("Olive", 0x70, 0x8D, 0x23),
                new ColorName("Or (couleur)", 0xFF, 0xD7, 0x0),
                new ColorName("Orange brûlé", 0xCC, 0x55, 0x0),
                new ColorName("Orchidée", 0xDA, 0x70, 0xD6),
                new ColorName("Orpiment (pigment)", 0xFC, 0xD2, 0x1C),
                new ColorName("Paille", 0xFE, 0xE3, 0x47),
                new ColorName("Parme", 0xCF, 0xA0, 0xE9),
                new ColorName("Pelure d'oignon", 0xD5, 0x84, 0x90),
                new ColorName("Pervenche", 0xCC, 0xCC, 0xFF),
                new ColorName("Pistache", 0xBE, 0xF5, 0x74),
                new ColorName("Poil de chameau", 0xB6, 0x78, 0x23),
                new ColorName("Ponceau ou Coquelicot", 0xC6, 0x8, 0x0),
                new ColorName("Pourpre (héraldique)", 0x9E, 0x0E, 0x40),
                new ColorName("Prasin", 0x4C, 0xA6, 0x6B),
                new ColorName("Prune", 0x81, 0x14, 0x53),
                new ColorName("Puce", 0x4E, 0x16, 0x9),
                new ColorName("Rose Mountbatten", 0x99, 0x7A, 0x8D),
                new ColorName("Rouge anglais (pigment)", 0xF7, 0x23, 0x0C),
                new ColorName("Rouge cardinal", 0xB8, 0x20, 0x10),
                new ColorName("Rouge cerise", 0xBB, 0x0B, 0x0B),
                new ColorName("Rouge d'Andrinople (teinture)", 0xA9, 0x11, 0x1),
                new ColorName("Rouge de Falun (pigment)", 0x80, 0x18, 0x18),
                new ColorName("Rouge feu", 0xFF, 0x49, 0x1),
                new ColorName("Rouge indien (pigment)", 0xCD, 0x5C, 0x5C),
                new ColorName("Rouge sang", 0x85, 0x6, 0x6),
                new ColorName("Rouge tomette", 0xAE, 0x4A, 0x34),


                // Nuances de rouge récupérées sur internet
                
                new ColorName("Corail clair", 0xF0, 0x80, 0x80),
                new ColorName("Corail", 0xFF, 0x7F, 0x50),
                new ColorName("Tomate", 0xFF, 0x63, 0x47),
                new ColorName("Rouge capucine", 0xFF, 0x5E, 0x4D),
                new ColorName("Rouge", 0xFF, 0x00, 0x00),
                new ColorName("Vermeil", 0xFF, 0x09, 0x21),
                new ColorName("Rouge feu", 0xFE, 0x1B, 0x00),
                new ColorName("Rouge anglais", 0xF7, 0x23, 0x0C),
                new ColorName("Grenadine", 0xE9, 0x38, 0x3F),
                new ColorName("Écarlate", 0xED, 0x00, 0x00),
                new ColorName("Tomate", 0xDE, 0x29, 0x16),
                new ColorName("Vermillon", 0xDB, 0x17, 0x02),
                new ColorName("Rouge Indien", 0xCD, 0x5C, 0x5C),
                new ColorName("Tomette", 0xAE, 0x4A, 0x34),
                new ColorName("Rouge cramoisi", 0xDC, 0x14, 0x3C),
                new ColorName("Groseille", 0xCF, 0x0A, 0x1D),
                new ColorName("Fraise", 0xBF, 0x30, 0x30),
                new ColorName("Rouge cardinal", 0xB8, 0x20, 0x10),
                new ColorName("Cerise", 0xBB, 0x0B, 0x0B),
                new ColorName("Coquelicot", 0xC6, 0x08, 0x00),
                new ColorName("Ecrevisse", 0xBC, 0x20, 0x01),
                new ColorName("Ambre rouge", 0xAD, 0x39, 0x0E),
                new ColorName("Rouge brique", 0xB2, 0x22, 0x22),
                new ColorName("Brun", 0xA5, 0x2A, 0x2A),
                new ColorName("Rouge carmin", 0x96, 0x00, 0x18),
                new ColorName("Rouge foncé", 0x8B, 0x00, 0x00),
                new ColorName("Sang", 0x85, 0x06, 0x06),
                new ColorName("Bordeaux", 0x80, 0x00, 0x00),
                new ColorName("Grenat", 0x6E, 0x0B, 0x14),

                // Fin nuances de rouge 

                new ColorName("Rouille", 0x98, 0x57, 0x17),
                new ColorName("Roux", 0xAD, 0x4F, 0x9),
                new ColorName("Rubis", 0xE0, 0x11, 0x5F),
                new ColorName("Sable", 0xE0, 0xCD, 0xA9),
                new ColorName("Sable (héraldique)", 0x0, 0x0, 0x0),
                new ColorName("Safre", 0x1, 0x31, 0xB4),
                new ColorName("Sang de bœuf", 0x73, 0x8, 0x0),
                new ColorName("Sanguine", 0x85, 0x6, 0x6),
                new ColorName("Saphir", 0x1, 0x31, 0xB4),
                new ColorName("Sarcelle", 0x0, 0x80, 0x80),
                new ColorName("Saumon", 0xF8, 0x8E, 0x55),
                new ColorName("Sépia", 0xAE, 0x89, 0x64),
                new ColorName("Sinople (héraldique)", 0x14, 0x94, 0x14),
                new ColorName("Smalt", 0x0, 0x33, 0x99),
                new ColorName("Soufre", 0xFF, 0xFF, 0x6B),
                new ColorName("Tabac", 0x9F, 0x55, 0x1E),
                new ColorName("Taupe", 0x46, 0x3F, 0x32),
                new ColorName("Terre d'ombre", 0x92, 0x6D, 0x27),
                new ColorName("Tomate", 0xDE, 0x29, 0x16),
                new ColorName("Topaze", 0xFA, 0xEA, 0x73),
                new ColorName("Tourterelle ou Colombin", 0xBB, 0xAC, 0xAC),
                new ColorName("Turquoise", 0x25, 0xFD, 0xE9),
                new ColorName("Vanille", 0xE1, 0xCE, 0x9A),
                new ColorName("Vermeil", 0xFF, 0x9, 0x21),
                new ColorName("Vermillon", 0xDB, 0x17, 0x2),
                new ColorName("Vert bouteille", 0x9, 0x6A, 0x9),
                new ColorName("Vert céladon", 0x83, 0xA6, 0x97),
                new ColorName("Vert d'eau", 0xB0, 0xF2, 0xB6),
                new ColorName("Vert de chrome", 0x18, 0x39, 0x1E),
                new ColorName("Vert-de-gris", 0x95, 0xA5, 0x95),
                new ColorName("Vert de Hooker", 0x1B, 0x4F, 0x8),
                new ColorName("Vert de sève", 0x50, 0x7d, 0x2A),
                new ColorName("Vert de vessie", 0x22, 0x78, 0x0F),
                new ColorName("Vert émeraude ou Smaragdin (RAL 6001)", 0x36, 0x67, 0x35),
                new ColorName("Vert épinard", 0x17, 0x57, 0x32),
                new ColorName("Vert impérial", 0x0, 0x56, 0x1B),
                new ColorName("Vert inde", 0x13, 0x88, 0x08),
                new ColorName("Vert lichen", 0x85, 0xC1, 0x7E),
                new ColorName("Vert olive", 0x55, 0x6B, 0x2F),
                new ColorName("Vert perroquet", 0x3A, 0xF2, 0x4B),
                new ColorName("Vert poireau", 0x4C, 0xA6, 0x6B),
                new ColorName("Vert pomme", 0x34, 0xC9, 0x24),
                new ColorName("Vert prairie", 0x57, 0xD5, 0x3B),
                new ColorName("Vert printemps", 0x0, 0xFF, 0x7F),
                new ColorName("Vert sapin", 0x9, 0x52, 0x28),
                new ColorName("Vert sauge", 0x68, 0x9D, 0x71),
                new ColorName("Vert tilleul", 0xA5, 0xD1, 0x52),
                new ColorName("Vert Véronèse", 0x5A, 0x65, 0x21),

                // Couleurs vert complémentaires  récupérer sur internet
                new ColorName("Blanc menthe", 0xF5, 0xFF, 0xFA),
                new ColorName("Miellat", 0xF0, 0xFF, 0xF0),
                new ColorName("Vert d'eau", 0xB0, 0xF2, 0xB6),
                new ColorName("Vert pâle", 0x98, 0xFB, 0x98),
                new ColorName("Vert clair", 0x90, 0xEE, 0x90),
                new ColorName("Pistache", 0xBE, 0xF5, 0x74),
                new ColorName("Anis", 0x9F, 0xE8, 0x55),
                new ColorName("Vert tilleul", 0xA5, 0xD1, 0x52),
                new ColorName("Absinthe", 0x7F, 0xDD, 0x4C),
                new ColorName("Vert chartreuse", 0xC2, 0xF7, 0x32),
                new ColorName("Vert jaune", 0xAD, 0xFF, 0x2F),
                new ColorName("Chartreuse", 0x7F, 0xFF, 0x00),
                new ColorName("Vert prairie", 0x7C, 0xFC, 0x00),
                new ColorName("Citron vert", 0x00, 0xFF, 0x00),
                new ColorName("Citron vert foncé", 0x32, 0xCD, 0x32),
                new ColorName("Lichen", 0x85, 0xC1, 0x7E),
                new ColorName("Vert amande", 0x82, 0xC4, 0x6C),
                new ColorName("Brun kaki foncé", 0xBD, 0xB7, 0x6B),
                new ColorName("Jaune vert", 0x9A, 0xCD, 0x32),
                new ColorName("Vert opaline", 0x97, 0xDF, 0xC6),
                new ColorName("Jade", 0x87, 0xE9, 0x90),
                new ColorName("Menthe à l'eau", 0x54, 0xF9, 0x8D),
                new ColorName("Vert printemps", 0x00, 0xFF, 0x7F),
                new ColorName("Vert printemps moyen", 0x00, 0xFA, 0x9A),
                new ColorName("Asperge", 0x7B, 0xA0, 0x5B),
                new ColorName("Vert mousse", 0x67, 0x9F, 0x5A),
                new ColorName("Vert sauge", 0x68, 0x9D, 0x71),
                new ColorName("Vert océan moyen", 0x3C, 0xB3, 0x71),
                new ColorName("Vert poireau", 0x4C, 0xA6, 0x6B),
                new ColorName("Vert océan", 0x2E, 0x8B, 0x57),
                new ColorName("Malachite", 0x1F, 0xA0, 0x55),
                new ColorName("Menthe", 0x16, 0xB8, 0x4E),
                new ColorName("Vert émeraude", 0x01, 0xD7, 0x58),
                new ColorName("Vert perroquet", 0x3A, 0xF2, 0x4B),
                new ColorName("Vert prairie", 0x57, 0xD5, 0x3B),
                new ColorName("Vert pomme", 0x34, 0xC9, 0x24),
                new ColorName("Vert gazon", 0x3A, 0x9D, 0x23),
                new ColorName("Vert forêt", 0x22, 0x8B, 0x22),
                new ColorName("Vert", 0x00, 0x80, 0x00),
                new ColorName("Vert bouteille", 0x09, 0x6A, 0x09),
                new ColorName("Vert foncé", 0x00, 0x64, 0x00),
                new ColorName("Vert impérial", 0x00, 0x56, 0x1B),
                new ColorName("Vert sapin", 0x09, 0x52, 0x28),
                new ColorName("Vert épinard", 0x17, 0x57, 0x32),
                new ColorName("Vert kaki", 0x79, 0x89, 0x33),
                new ColorName("Kaki", 0x6B, 0x8E, 0x23),
                new ColorName("Olive", 0x80, 0x80, 0x00),
                new ColorName("Avocat", 0x56, 0x82, 0x03),
                new ColorName("Vert olive foncé", 0x55, 0x6B, 0x2F),
                new ColorName("Vert militaire", 0x59, 0x66, 0x43),
                new ColorName("Vert Véronèse", 0x5A, 0x65, 0x21),
                new ColorName("Vert océan foncé", 0x8F, 0xBC, 0x8F),
                new ColorName("Vert-de-gris", 0x95, 0xA5, 0x95),
                new ColorName("Vert céladon", 0x83, 0xA6, 0x97),
                new ColorName("Vert pin", 0x01, 0x79, 0x6F),
                // Fin autres nuances de vert 





                new ColorName("Violet", 0x88, 0x6, 0xCE),
                new ColorName("Violet d'évêque", 0x72, 0x3E, 0x64),
                new ColorName("Viride", 0x40, 0x82, 0x6D),
                new ColorName("Zinzolin", 0x6C, 0x2, 0x77)
            };
            #endregion

            #region Couleurs en anglais


            /*couleurs.Add(new ColorName("AliceBlue", 0xF0, 0xF8, 0xFF));
            couleurs.Add(new ColorName("AntiqueWhite", 0xFA, 0xEB, 0xD7));
            couleurs.Add(new ColorName("Aqua", 0x00, 0xFF, 0xFF));
            couleurs.Add(new ColorName("Aquamarine", 0x7F, 0xFF, 0xD4));
            couleurs.Add(new ColorName("Azure", 0xF0, 0xFF, 0xFF));
            couleurs.Add(new ColorName("Beige", 0xF5, 0xF5, 0xDC));
            couleurs.Add(new ColorName("Bisque", 0xFF, 0xE4, 0xC4));
            couleurs.Add(new ColorName("Black", 0x00, 0x00, 0x00));
            couleurs.Add(new ColorName("BlanchedAlmond", 0xFF, 0xEB, 0xCD));
            couleurs.Add(new ColorName("Blue", 0x00, 0x00, 0xFF));
            couleurs.Add(new ColorName("BlueViolet", 0x8A, 0x2B, 0xE2));
            couleurs.Add(new ColorName("Brown", 0xA5, 0x2A, 0x2A));
            couleurs.Add(new ColorName("BurlyWood", 0xDE, 0xB8, 0x87));
            couleurs.Add(new ColorName("CadetBlue", 0x5F, 0x9E, 0xA0));
            couleurs.Add(new ColorName("Chartreuse", 0x7F, 0xFF, 0x00));
            couleurs.Add(new ColorName("Chocolate", 0xD2, 0x69, 0x1E));
            couleurs.Add(new ColorName("Coral", 0xFF, 0x7F, 0x50));
            couleurs.Add(new ColorName("CornflowerBlue", 0x64, 0x95, 0xED));
            couleurs.Add(new ColorName("Cornsilk", 0xFF, 0xF8, 0xDC));
            couleurs.Add(new ColorName("Crimson", 0xDC, 0x14, 0x3C));
            couleurs.Add(new ColorName("Cyan", 0x00, 0xFF, 0xFF));
            couleurs.Add(new ColorName("DarkBlue", 0x00, 0x00, 0x8B));
            couleurs.Add(new ColorName("DarkCyan", 0x00, 0x8B, 0x8B));
            couleurs.Add(new ColorName("DarkGoldenRod", 0xB8, 0x86, 0x0B));
            couleurs.Add(new ColorName("DarkGray", 0xA9, 0xA9, 0xA9));
            couleurs.Add(new ColorName("DarkGreen", 0x00, 0x64, 0x00));
            couleurs.Add(new ColorName("DarkKhaki", 0xBD, 0xB7, 0x6B));
            couleurs.Add(new ColorName("DarkMagenta", 0x8B, 0x00, 0x8B));
            couleurs.Add(new ColorName("DarkOliveGreen", 0x55, 0x6B, 0x2F));
            couleurs.Add(new ColorName("DarkOrange", 0xFF, 0x8C, 0x00));
            couleurs.Add(new ColorName("DarkOrchid", 0x99, 0x32, 0xCC));
            couleurs.Add(new ColorName("DarkRed", 0x8B, 0x00, 0x00));
            couleurs.Add(new ColorName("DarkSalmon", 0xE9, 0x96, 0x7A));
            couleurs.Add(new ColorName("DarkSeaGreen", 0x8F, 0xBC, 0x8F));
            couleurs.Add(new ColorName("DarkSlateBlue", 0x48, 0x3D, 0x8B));
            couleurs.Add(new ColorName("DarkSlateGray", 0x2F, 0x4F, 0x4F));
            couleurs.Add(new ColorName("DarkTurquoise", 0x00, 0xCE, 0xD1));
            couleurs.Add(new ColorName("DarkViolet", 0x94, 0x00, 0xD3));
            couleurs.Add(new ColorName("DeepPink", 0xFF, 0x14, 0x93));
            couleurs.Add(new ColorName("DeepSkyBlue", 0x00, 0xBF, 0xFF));
            couleurs.Add(new ColorName("DimGray", 0x69, 0x69, 0x69));
            couleurs.Add(new ColorName("DodgerBlue", 0x1E, 0x90, 0xFF));
            couleurs.Add(new ColorName("FireBrick", 0xB2, 0x22, 0x22));
            couleurs.Add(new ColorName("FloralWhite", 0xFF, 0xFA, 0xF0));
            couleurs.Add(new ColorName("ForestGreen", 0x22, 0x8B, 0x22));
            couleurs.Add(new ColorName("Fuchsia", 0xFF, 0x00, 0xFF));
            couleurs.Add(new ColorName("Gainsboro", 0xDC, 0xDC, 0xDC));
            couleurs.Add(new ColorName("GhostWhite", 0xF8, 0xF8, 0xFF));
            couleurs.Add(new ColorName("Gold", 0xFF, 0xD7, 0x00));
            couleurs.Add(new ColorName("GoldenRod", 0xDA, 0xA5, 0x20));
            couleurs.Add(new ColorName("Gray", 0x80, 0x80, 0x80));
            couleurs.Add(new ColorName("Green", 0x00, 0x80, 0x00));
            couleurs.Add(new ColorName("GreenYellow", 0xAD, 0xFF, 0x2F));
            couleurs.Add(new ColorName("HoneyDew", 0xF0, 0xFF, 0xF0));
            couleurs.Add(new ColorName("HotPink", 0xFF, 0x69, 0xB4));
            couleurs.Add(new ColorName("IndianRed", 0xCD, 0x5C, 0x5C));
            couleurs.Add(new ColorName("Indigo", 0x4B, 0x00, 0x82));
            couleurs.Add(new ColorName("Ivory", 0xFF, 0xFF, 0xF0));
            couleurs.Add(new ColorName("Khaki", 0xF0, 0xE6, 0x8C));
            couleurs.Add(new ColorName("Lavender", 0xE6, 0xE6, 0xFA));
            couleurs.Add(new ColorName("LavenderBlush", 0xFF, 0xF0, 0xF5));
            couleurs.Add(new ColorName("LawnGreen", 0x7C, 0xFC, 0x00));
            couleurs.Add(new ColorName("LemonChiffon", 0xFF, 0xFA, 0xCD));
            couleurs.Add(new ColorName("LightBlue", 0xAD, 0xD8, 0xE6));
            couleurs.Add(new ColorName("LightCoral", 0xF0, 0x80, 0x80));
            couleurs.Add(new ColorName("LightCyan", 0xE0, 0xFF, 0xFF));
            couleurs.Add(new ColorName("LightGoldenRodYellow", 0xFA, 0xFA, 0xD2));
            couleurs.Add(new ColorName("LightGray", 0xD3, 0xD3, 0xD3));
            couleurs.Add(new ColorName("LightGreen", 0x90, 0xEE, 0x90));
            couleurs.Add(new ColorName("LightPink", 0xFF, 0xB6, 0xC1));
            couleurs.Add(new ColorName("LightSalmon", 0xFF, 0xA0, 0x7A));
            couleurs.Add(new ColorName("LightSeaGreen", 0x20, 0xB2, 0xAA));
            couleurs.Add(new ColorName("LightSkyBlue", 0x87, 0xCE, 0xFA));
            couleurs.Add(new ColorName("LightSlateGray", 0x77, 0x88, 0x99));
            couleurs.Add(new ColorName("LightSteelBlue", 0xB0, 0xC4, 0xDE));
            couleurs.Add(new ColorName("LightYellow", 0xFF, 0xFF, 0xE0));
            couleurs.Add(new ColorName("Lime", 0x00, 0xFF, 0x00));
            couleurs.Add(new ColorName("LimeGreen", 0x32, 0xCD, 0x32));
            couleurs.Add(new ColorName("Linen", 0xFA, 0xF0, 0xE6));
            couleurs.Add(new ColorName("Magenta", 0xFF, 0x00, 0xFF));
            couleurs.Add(new ColorName("Maroon", 0x80, 0x00, 0x00));
            couleurs.Add(new ColorName("MediumAquaMarine", 0x66, 0xCD, 0xAA));
            couleurs.Add(new ColorName("MediumBlue", 0x00, 0x00, 0xCD));
            couleurs.Add(new ColorName("MediumOrchid", 0xBA, 0x55, 0xD3));
            couleurs.Add(new ColorName("MediumPurple", 0x93, 0x70, 0xDB));
            couleurs.Add(new ColorName("MediumSeaGreen", 0x3C, 0xB3, 0x71));
            couleurs.Add(new ColorName("MediumSlateBlue", 0x7B, 0x68, 0xEE));
            couleurs.Add(new ColorName("MediumSpringGreen", 0x00, 0xFA, 0x9A));
            couleurs.Add(new ColorName("MediumTurquoise", 0x48, 0xD1, 0xCC));
            couleurs.Add(new ColorName("MediumVioletRed", 0xC7, 0x15, 0x85));
            couleurs.Add(new ColorName("MidnightBlue", 0x19, 0x19, 0x70));
            couleurs.Add(new ColorName("MintCream", 0xF5, 0xFF, 0xFA));
            couleurs.Add(new ColorName("MistyRose", 0xFF, 0xE4, 0xE1));
            couleurs.Add(new ColorName("Moccasin", 0xFF, 0xE4, 0xB5));
            couleurs.Add(new ColorName("NavajoWhite", 0xFF, 0xDE, 0xAD));
            couleurs.Add(new ColorName("Navy", 0x00, 0x00, 0x80));
            couleurs.Add(new ColorName("OldLace", 0xFD, 0xF5, 0xE6));
            couleurs.Add(new ColorName("Olive", 0x80, 0x80, 0x00));
            couleurs.Add(new ColorName("OliveDrab", 0x6B, 0x8E, 0x23));
            couleurs.Add(new ColorName("Orange", 0xFF, 0xA5, 0x00));
            couleurs.Add(new ColorName("OrangeRed", 0xFF, 0x45, 0x00));
            couleurs.Add(new ColorName("Orchid", 0xDA, 0x70, 0xD6));
            couleurs.Add(new ColorName("PaleGoldenRod", 0xEE, 0xE8, 0xAA));
            couleurs.Add(new ColorName("PaleGreen", 0x98, 0xFB, 0x98));
            couleurs.Add(new ColorName("PaleTurquoise", 0xAF, 0xEE, 0xEE));
            couleurs.Add(new ColorName("PaleVioletRed", 0xDB, 0x70, 0x93));
            couleurs.Add(new ColorName("PapayaWhip", 0xFF, 0xEF, 0xD5));
            couleurs.Add(new ColorName("PeachPuff", 0xFF, 0xDA, 0xB9));
            couleurs.Add(new ColorName("Peru", 0xCD, 0x85, 0x3F));
            couleurs.Add(new ColorName("Pink", 0xFF, 0xC0, 0xCB));
            couleurs.Add(new ColorName("Plum", 0xDD, 0xA0, 0xDD));
            couleurs.Add(new ColorName("PowderBlue", 0xB0, 0xE0, 0xE6));
            couleurs.Add(new ColorName("Purple", 0x80, 0x00, 0x80));
            couleurs.Add(new ColorName("Red", 0xFF, 0x00, 0x00));
            couleurs.Add(new ColorName("RosyBrown", 0xBC, 0x8F, 0x8F));
            couleurs.Add(new ColorName("RoyalBlue", 0x41, 0x69, 0xE1));
            couleurs.Add(new ColorName("SaddleBrown", 0x8B, 0x45, 0x13));
            couleurs.Add(new ColorName("Salmon", 0xFA, 0x80, 0x72));
            couleurs.Add(new ColorName("SandyBrown", 0xF4, 0xA4, 0x60));
            couleurs.Add(new ColorName("SeaGreen", 0x2E, 0x8B, 0x57));
            couleurs.Add(new ColorName("SeaShell", 0xFF, 0xF5, 0xEE));
            couleurs.Add(new ColorName("Sienna", 0xA0, 0x52, 0x2D));
            couleurs.Add(new ColorName("Silver", 0xC0, 0xC0, 0xC0));
            couleurs.Add(new ColorName("SkyBlue", 0x87, 0xCE, 0xEB));
            couleurs.Add(new ColorName("SlateBlue", 0x6A, 0x5A, 0xCD));
            couleurs.Add(new ColorName("SlateGray", 0x70, 0x80, 0x90));
            couleurs.Add(new ColorName("Snow", 0xFF, 0xFA, 0xFA));
            couleurs.Add(new ColorName("SpringGreen", 0x00, 0xFF, 0x7F));
            couleurs.Add(new ColorName("SteelBlue", 0x46, 0x82, 0xB4));
            couleurs.Add(new ColorName("Tan", 0xD2, 0xB4, 0x8C));
            couleurs.Add(new ColorName("Teal", 0x00, 0x80, 0x80));
            couleurs.Add(new ColorName("Thistle", 0xD8, 0xBF, 0xD8));
            couleurs.Add(new ColorName("Tomato", 0xFF, 0x63, 0x47));
            couleurs.Add(new ColorName("Turquoise", 0x40, 0xE0, 0xD0));
            couleurs.Add(new ColorName("Violet", 0xEE, 0x82, 0xEE));
            couleurs.Add(new ColorName("Wheat", 0xF5, 0xDE, 0xB3));
            couleurs.Add(new ColorName("White", 0xFF, 0xFF, 0xFF));
            couleurs.Add(new ColorName("WhiteSmoke", 0xF5, 0xF5, 0xF5));
            couleurs.Add(new ColorName("Yellow", 0xFF, 0xFF, 0x00));
            couleurs.Add(new ColorName("YellowGreen", 0x9A, 0xCD, 0x32));        */
            #endregion 

            return couleurs;
        }

        /// <summary>
        /// Looks for thr closest color givibg R G and B values 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns> A string conataining the color nameof the closest value from the color list  or an error message </returns>
        public String GetColorNameFromRgb(int r, int g, int b)
        {

            String closestMatch = "Couleur non trouvée."; // Initialisation with error message in case we don't find a matching value
                                                          // and to avoid error message "use of unassigned local variable..."????
            int minRGBDist = int.MaxValue;  // initialisation of  minRGBDist to max interger value so that RGB distance to first list element
                                            // will be less than its actual value
            int rgbdist;

            foreach (ColorName c in colorsList)
            {
                rgbdist = c.DistanceRGB(r, g, b);
                if (rgbdist < minRGBDist)
                {
                    minRGBDist = rgbdist;
                    closestMatch = c.Nom;
                }
                // If values match, (minRGBDist = 0`) then no need to continue loop
                if (minRGBDist == 0)
                    break;
            }
            return closestMatch;
        }

        public void GessColor()
        {
            ColorDisplayName = GetColorNameFromRgb(_red, _green, _blue);
        }

        /*public async Task TellColorAsync()
        {

           
            ITextToSpeechService service = DependencyService.Get<ITextToSpeechService>(DependencyFetchTarget.NewInstance);
            using (service as IDisposable)
            {
                await service.SpeakAsync(ColorDisplayName);
            }
           
        } */
    }


    public class ColorName

    {
        private string _nom;
        private byte _r;
        private byte _g;
        private byte _b;

        public string Nom { get { return _nom; } set { _nom = value; } }
        public byte Red { get { return _r; } set { _r = value; } }
        public byte Green { get { return _g; } set { _g = value; } }
        public byte Blue { get { return _b; } set { _b = value; } }

        public ColorName()
        {
            Nom = "White";
            Red = 255;
            Green = 255;
            Blue = 255;
        }

        public ColorName(string nom, byte r, byte g, byte b)
        {
            this.Nom = nom;
            this.Red = r;
            this.Green = g;
            this.Blue = b;
        }

        /// <summary>
        /// On calcule la distace entre la couleur donnée et les couleurs connues
        /// celle dont la distance est la plus petite sera le couleur rapprochée
        /// Voir la formule pour calculer la distace 
        ///     Distance RGB = abs(myColor.Red - colorI.Red) + 
        ///                   abs(myColor.Red - colorI.Grenn) +
        ///                   abs(myColor.Blue - colorI.Blue)  
        /// </summary>
        /// <param name="pixR"> Valeur du rouge</param>
        /// <param name="pixG"> Valeur du vert</param>
        /// <param name="pixB"> Valeur du bleue</param>
        /// <returns></returns>
        public int DistanceRGB(int pixR, int pixG, int pixB)
        {
            return (int)((Math.Abs(pixR - Red) + Math.Abs(pixG - Green) + Math.Abs(pixB - Blue)));
        }

    }


}
