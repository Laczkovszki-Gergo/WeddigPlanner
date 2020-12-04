using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eskuvo_tervezo.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Advices : Page
    {
        Windows.Functions f = new Windows.Functions();

        object rm;
        string[] ResourceNames;

        public Advices(Models.Login _User, ResourceManager _rm, string[] _Resourcenames)
        {
            InitializeComponent();
            rm = _rm;
            ResourceNames = _Resourcenames;

            Tbl_Advices.TextWrapping = TextWrapping.WrapWithOverflow; 
            Tbl_Advices.Text = null;
            Tbl_Advices.Inlines.Add(new Run("Ha megtörtént az eljegyzés és már azonnal bele akartok fogni az esküvő \nszervezésébe pár fontos teendő, amin el is kezdhettek gondolkodni.\n\n") { FontWeight = FontWeights.Bold, FontSize = 30});
            Tbl_Advices.Inlines.Add(
    "o Beszéljétek meg az esküvő dátumát.\n" +
    "o Beszéljétek meg a költségkeretet, amit az esküvő szeretnétek szánni és hogy ki mennyit tud magára vállalni.\n" +
    "o Esküvői meghívón kezdjetek el gondolkodni.\n" +
    "o Készítsetek egy kezdeti vendéglistát, hogy hány főt szeretnétek a szertartásokon, illetve a lakodalmi részen vendégül látni.\n" +
    "  Ennek a körülbelüli számnak jelentősége lesz a helyszínválasztásnál, hisz minden vendéglátó helyszínnek más - más a befogadóképessége.\n" +
    "o Kezdjetek el esküvői helyszíneket nézni (itt már fontos, hogy a dátumról legyen elképzelésetek) Ha felkapott helyszínt néztetek ki\n" +
    "  magatoknak abban az esetben érdemes dátumilag egy B opciót is választani. Esetleg tegyétek fel magatoknak azt a kérdést, hogy a helyszínhez\n" +
    "  ragaszkodtok vagy a dátumhoz.\n" +
    "o A legfontosabb, hogy a szervezés úgy történjen, hogy minden kiválasztott szolgáltató vagy helyszín nektek legyen jó és nem másnak, hisz ez a\n" +
    "  nap csak is rólatok szól.\n" +
    "o Ha megvan a dátumotok és a helyszín is ha csak elképzelésileg is, de a következő fontos lépés, hogy a polgári szertartáshoz egyeztessetek\n" +
    "  az anyakönyvi hivatallal abban a városban ahol tervezitek ezt a szertartást megtartani.\n" +
    "o Foglaljátok le az esküvői fogadás, lakodalom helyszínét és ezt egyidejűleg beszéljétek meg a menüopciókat és ennek költségeit, valamint az\n" +
    "  italköltségeket is.\n" +
    "o Válasszátok ki a tanúitokat és a koszorúslányokat.\n" +
    "o Keressetek esküvői fotóst, vőfélyt, Dj - t / zenekart, dekorost (fontos a szolgáltatókat minél hamarabb lefoglalni, hisz ebben a helyzetben\n" +
    "  verseny megy a legjobb szolgáltatókért, de ha sikerül mindent időben lefoglalni félelemre semmi ok. De ha mégsem sikerül akkor sem aggódj hisz\n" +
    "  meg fogod találni a tökéletest minden esetben.\n" +
    "o Szolgáltatók esetében érdemes szerződést kötni, hogy le legyetek fedve.\n" +
    "o Válasszatok közösen esküvői gyűrűt! Fontos tudni, hogy nem feltétlenül készen tudsz csak vásárolni hanem a saját elképzelésed alapján\n" +
    "  el is tudod készítettni.\n" +
    "o Kezdj el esküvői ruhákat nézelődni. Ha több szalon kínálatát is meg szeretnéd nézni fontos, hogy a ruhapróbát próbáld egy napra betenni,\n" +
    "  hogy nehogy utólag bánd meg, hogy elvitték az általad áhított ruhát! De az a legfontosabb, hogy te jól érzed magad a ruhádban.\n" +
    "  A vőlegénynek az öltözetére több idő is elegendő, de azt se hagyjátok az utolsó pillanatokra.\n" +
    "o Ha ezekkel megvagy akkor már nagyjából meg is szervezted az esküvődet!\n\n");
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 4 - 8 hónappal\n\n") { FontWeight = FontWeights.Bold, FontSize = 26 });
            Tbl_Advices.Inlines.Add(
    "o Véglegesítsétek le a meghívottak listáját.\n" +
    "o Rendeljétek meg a meghívókat figyelve arra, hogy külön meghívót rendeljetek azoknak akik csak a szertartásra vannak meghívva és azoknak a\n" +
    "  meghívottaknak akik a lakodalomra is.  Küldjétek ki a meghívókat.\n" +
    "o Rendeljétek meg az esküvői tortát.\n" +
    "o Szerezzétek be a vőlegény ruháját, de ha a menyasszony ruhája sincs beszerezve még az sem késő.\n" +
    "o Foglaljatok szállást a nászéjszakára és / vagy a nászútra.\n" +
    "o Kezdjétek el megszervezni a nagy nap programtervét / egyeztessetek a vőféllyel.\n" +
    "o Tervezzétek meg, hogy szükséges - e szertartás után fogadást tartani. Szem előtt tartva azokat, akik csak a szertartásra hivatalosak.\n" +
    "o Foglaljátok le az esküvői autót.\n" +
    "o Beszéljétek meg a dekorossal, hogy a virágokat ő intézi vagy nektek kell. Ha nektek kell akkor most van annak az ideje, hogy egyeztessetek\n" +
    "  a virágossal.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 2 - 3 hónappal\n\n") { FontWeight = FontWeights.Bold, FontSize = 26 });
            Tbl_Advices.Inlines.Add(
    "o Keressetek fodrászt és sminkest és foglaljátok le. (Érdemes próba sminket és próba hajat készíttetni.)\n" +
    "o A szertartásra szükséges zenék lefixálása és a DJ - vel és a zenekarral is legyen egy megerősítő megbeszélés.\n" +
    "o Véglegesítsétek le az esküvő minden egyes pontját, mind a programban, mind szolgáltatók oldaláról is.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 1 hónappal\n\n") { FontWeight = FontWeights.Bold, FontSize = 26 });
            Tbl_Advices.Inlines.Add(
  "o Utolsó jóvágyás a szolgáltatókkal, helyszínnel.\n" +
  "o Készítsetek ülésrendet.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 2 héttel\n\n") { FontWeight = FontWeights.Bold, FontSize = 26 });
            Tbl_Advices.Inlines.Add(
  "o Utolsó ruhapróba.\n" +
  "o Erősítsétek meg hogy minden rendelésetek rendben van - e.\n" +
  "o Fodrász, Sminkes, Körmös időpont véglegesítés.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 1 héttel\n\n") { FontWeight = FontWeights.Bold, FontSize = 26 });
            Tbl_Advices.Inlines.Add(
  "o Egyeztessetek a helyszínnel a végleges létszámról és a foglalt szobákról, ha erre szükség van.\n" +
  "o Beszéljétek át mégegyszer, hogy mindenki tudja e pontosan az általa elvállalt feladat menetét.\n" +
  "o Szolgáltatókkal való egyeztetés.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtti nap\n\n") { FontWeight = FontWeights.Bold, FontSize = 26 });
            Tbl_Advices.Inlines.Add(
    "o Kezdjétek el a dekorálást ha ti csináljátok, ha dekoros akkor egyeztessétek le vele a pontos elképzelést.\n" +
    "o Torták és sütemények szállítása a helyszínre.\n" +
    "o Pakoljatok össze mindent ami a nagy napra kell.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Nagy nap\n\n") { FontWeight = FontWeights.Bold, FontSize = 26 });
            Tbl_Advices.Inlines.Add(
   "o Legyen mindenre időtök ezért figyeljetek oda a csúszások kikerülése érdekében.\n" +
   "o Élvezzétek ez a ti napotok!\n\n");
            Tbl_Advices.Inlines.Add(new Run("Esküvő után:\n\n") { FontWeight = FontWeights.Bold, FontSize = 26 });
            Tbl_Advices.Inlines.Add(
    "o Intézzétek az esküvői költségeket.\n" +
    "o Minden bérelt vagy kölcsönkért tárgyat juttassatok vissza.\n" +
    "o Esküvői ajándékok elszállítása.");


        }
    }
}
