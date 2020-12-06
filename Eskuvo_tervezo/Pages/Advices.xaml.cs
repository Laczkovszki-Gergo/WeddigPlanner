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
        bool hun = true;
        public Advices(bool _hun)
        {
            InitializeComponent();
            hun = _hun;

        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAdvices(hun);
        }
        internal void LoadAdvices(bool Hun)
        {
            Tbl_Advices.TextWrapping = TextWrapping.WrapWithOverflow;
            Tbl_Advices.Text = null;

            if(Hun)
            {
            Tbl_Advices.Inlines.Add(new Run("Ha megtörtént az eljegyzés és már azonnal bele akartok fogni az esküvő\n szervezésébe pár fontos teendő, amin el is kezdhettek gondolkodni.\n\n") { FontWeight = FontWeights.Bold, FontSize = 28 });
            Tbl_Advices.Inlines.Add(
    "o Beszéljétek meg az esküvő dátumát.\n" +
    "o Beszéljétek meg a költségkeretet, amit az esküvő szeretnétek szánni és hogy ki mennyit tud magára vállalni.\n" +
    "o Esküvői meghívón kezdjetek el gondolkodni.\n" +
    "o Készítsetek egy kezdeti vendéglistát, hogy hány főt szeretnétek a szertartásokon, illetve a lakodalmi részen vendégül látni.\n" +
    "  Ennek a körülbelüli számnak jelentősége lesz a helyszínválasztásnál, hisz minden vendéglátó helyszínnek más - más a\n" +
    "  befogadóképessége.\n"+
    "o Kezdjetek el esküvői helyszíneket nézni (itt már fontos, hogy a dátumról legyen elképzelésetek) Ha felkapott helyszínt\n" +
    "  néztetek ki magatoknak abban az esetben érdemes dátumilag egy B opciót is választani. Esetleg tegyétek fel magatoknak\n" +
    "  azt a kérdést, hogy a helyszínhez ragaszkodtok vagy a dátumhoz.\n" +
    "o A legfontosabb, hogy a szervezés úgy történjen, hogy minden kiválasztott szolgáltató vagy helyszín nektek legyen jó és\n" +
    "  nem másnak, hisz ez a nap csak is rólatok szól.\n" +
    "o Ha megvan a dátumotok és a helyszín is ha csak elképzelésileg is, de a következő fontos lépés, hogy a polgári szertartáshoz\n" +
    "  egyeztessetek az anyakönyvi hivatallal abban a városban ahol tervezitek ezt a szertartást megtartani.\n" +
    "o Foglaljátok le az esküvői fogadás, lakodalom helyszínét és ezt egyidejűleg beszéljétek meg a menüopciókat és ennek költségeit,\n" +
    "  valamint az italköltségeket is.\n" +
    "o Válasszátok ki a tanúitokat és a koszorúslányokat.\n" +
    "o Keressetek esküvői fotóst, vőfélyt, Dj - t / zenekart, dekorost (fontos a szolgáltatókat minél hamarabb lefoglalni, hisz\n" +
    "  ebben a helyzetben verseny megy a legjobb szolgáltatókért, de ha sikerül mindent időben lefoglalni félelemre semmi ok.\n" +
    "  De ha mégsem sikerül akkor sem aggódj hisz meg fogod találni a tökéletest minden esetben.\n" +
    "o Szolgáltatók esetében érdemes szerződést kötni, hogy le legyetek fedve.\n" +
    "o Válasszatok közösen esküvői gyűrűt! Fontos tudni, hogy nem feltétlenül készen tudsz csak vásárolni hanem a saját elképzelésed\n" +
    "  alapján el is tudod készítettni.\n" +
    "o Kezdj el esküvői ruhákat nézelődni. Ha több szalon kínálatát is meg szeretnéd nézni fontos, hogy a ruhapróbát próbáld egy\n" +
    "  napra betenni, hogy nehogy utólag bánd meg, hogy elvitték az általad áhított ruhát! De az a legfontosabb, hogy te jól érzed\n" +
    "  magad a ruhádban. A vőlegénynek az öltözetére több idő is elegendő, de azt se hagyjátok az utolsó pillanatokra.\n" +
    "o Ha ezekkel megvagy akkor már nagyjából meg is szervezted az esküvődet!\n\n");
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 4 - 8 hónappal\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
            Tbl_Advices.Inlines.Add(
    "o Véglegesítsétek le a meghívottak listáját.\n" +
    "o Rendeljétek meg a meghívókat figyelve arra, hogy külön meghívót rendeljetek azoknak akik csak a szertartásra vannak\n" +
    "  meghívva és azoknak a meghívottaknak akik a lakodalomra is.  Küldjétek ki a meghívókat.\n" +
    "o Rendeljétek meg az esküvői tortát.\n" +
    "o Szerezzétek be a vőlegény ruháját, de ha a menyasszony ruhája sincs beszerezve még az sem késő.\n" +
    "o Foglaljatok szállást a nászéjszakára és / vagy a nászútra.\n" +
    "o Kezdjétek el megszervezni a nagy nap programtervét / egyeztessetek a vőféllyel.\n" +
    "o Tervezzétek meg, hogy szükséges - e szertartás után fogadást tartani. Szem előtt tartva azokat, akik csak a szertartásra\n" +
    "  hivatalosak."+
    "o Foglaljátok le az esküvői autót.\n" +
    "o Beszéljétek meg a dekorossal, hogy a virágokat ő intézi vagy nektek kell. Ha nektek kell akkor most van annak az ideje, hogy\n" +
    "  egyeztessetek a virágossal.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 2 - 3 hónappal\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
            Tbl_Advices.Inlines.Add(
    "o Keressetek fodrászt és sminkest és foglaljátok le. (Érdemes próba sminket és próba hajat készíttetni.)\n" +
    "o A szertartásra szükséges zenék lefixálása és a DJ - vel és a zenekarral is legyen egy megerősítő megbeszélés.\n" +
    "o Véglegesítsétek le az esküvő minden egyes pontját, mind a programban, mind szolgáltatók oldaláról is.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 1 hónappal\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
            Tbl_Advices.Inlines.Add(
  "o Utolsó jóvágyás a szolgáltatókkal, helyszínnel.\n" +
  "o Készítsetek ülésrendet.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 2 héttel\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
            Tbl_Advices.Inlines.Add(
  "o Utolsó ruhapróba.\n" +
  "o Erősítsétek meg hogy minden rendelésetek rendben van - e.\n" +
  "o Fodrász, Sminkes, Körmös időpont véglegesítés.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtt 1 héttel\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
            Tbl_Advices.Inlines.Add(
  "o Egyeztessetek a helyszínnel a végleges létszámról és a foglalt szobákról, ha erre szükség van.\n" +
  "o Beszéljétek át mégegyszer, hogy mindenki tudja e pontosan az általa elvállalt feladat menetét.\n" +
  "o Szolgáltatókkal való egyeztetés.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Esküvő előtti nap\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
            Tbl_Advices.Inlines.Add(
    "o Kezdjétek el a dekorálást ha ti csináljátok, ha dekoros akkor egyeztessétek le vele a pontos elképzelést.\n" +
    "o Torták és sütemények szállítása a helyszínre.\n" +
    "o Pakoljatok össze mindent ami a nagy napra kell.\n\n"
                );
            Tbl_Advices.Inlines.Add(new Run("Nagy nap\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
            Tbl_Advices.Inlines.Add(
   "o Legyen mindenre időtök ezért figyeljetek oda a csúszások kikerülése érdekében.\n" +
   "o Élvezzétek ez a ti napotok!\n\n");
            Tbl_Advices.Inlines.Add(new Run("Esküvő után\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
            Tbl_Advices.Inlines.Add(
    "o Intézzétek az esküvői költségeket.\n" +
    "o Minden bérelt vagy kölcsönkért tárgyat juttassatok vissza.\n" +
    "o Esküvői ajándékok elszállítása.");
            }
            else
            {
            Tbl_Advices.Inlines.Add(new Run("If the engagement already took place and you immediately want to start organizing the\n wedding, here are some round of the duties on what you can already start thinking.\n\n") { FontWeight = FontWeights.Bold, FontSize = 28});
            Tbl_Advices.Inlines.Add(
    "o Talk about the date.\n" +
    "o Discuss about the costs what you plan to dedicate on the wedding by parts \n" +
    "o Start thinking about the wedding invitations.\n" +
    "o Make an initial guest list about the people you would like to see on your wedding and how much of them you plan to invite\n" +
    " on dinner. This guessed number is impactful when it comes to choosing the place itself, every host has different capacity.\n" +
    "o Start looking for wedding places (here you already have to know an approximate date) If you searched for hot places\n" +
    "  For yourselves, it is worth choosing an option B. Question yourself whether the place \n" +
    "  or the date is more important.\n" +
    "o The most important thing is to make sure that the organised place and services is perfect for you, not for others as this \n" +
    "  date is only about you two.\n" +
    "o If you have the date and the place, even if only theorically, the next important step is to organize the registry marriage\n" +
    "  with the register office in the city where you plan to hold it.\n" +
    "o Book the chosen spot and in the same time talk over about the menu \n" +
    "  and the drinks’ cost.\n" +
    "o Pick your testifiers and bridesmaids.\n" +
    "o Look for photographer, bridesman, a Dj / band, decorating person (it is important to book all the services as\n" +
    "  there is competition for the best ones, but in case you can book everything in time, you don’t have to fear.\n" +
    "  In case it doesn’t happen you can still find the perfect solutions.\n" +
    "o When it comes to services, it can help to sign a contract so that you are covered.\n" +
    "o Together, choose the rings! You have to know that you can order a special one, not only already-made ones.\n" +
    "  you can have it done by your needs.\n" +
    "o Start looking for wedding clothes. In case you want to search for more shops, put all the fitting ons on one day,\n" +
    "  so that you won’t regret that someone else already took your chosen one.The most important thing is to feel yourself\n " +
    "  comfortable in your cloth. For the fiancé, it is not so urgent but don’t keep that either for the last moments.\n" +
    "o If you finish with all these steps, your wedding is already organized!\n\n");
                Tbl_Advices.Inlines.Add(new Run("Before wedding 4 - 8 month\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
                Tbl_Advices.Inlines.Add(
        "o Finalize the invitation list.\n" +
        "o Order the invitation cards but take care to destine them separately for the ones who is only invited for the ritual and\n" +
        "  for the ones who is also invited for dinner.  Send out the cards.\n" +
        "o Order the wedding cake.\n" +
        "o Procure the fiancé’s suit, if the bride’s cloth is not yet yours, it is still not late.\n" +
        "o Book the accomodation for the wedding-night / honeymoon.\n" +
        "o Start organizing this BIG DAY’s daily programme and discuss it with the bridesman.\n" +
        "o Plan if there is a need for a reception. Concerning the ones invited only for the ritual.\n" +
        "o Book the wedding car.\n" +
        "o Talk with the decorating serviceperson if the flowers are managed or you have to do it. In case it’s your task,\n"+
        "  it is time to talk to the appropriate person.\n\n"
                    );
                Tbl_Advices.Inlines.Add(new Run("Before wedding 2 - 3 months\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
                Tbl_Advices.Inlines.Add(
        "o Look for hairdresser and makeup artist. (Maybe it’s a good idea to try both of them first.)\n" +
        "o There should be a strengthening talk with the DJ about the ritual’s background songs.\n" +
        "o Definitize every single point of the wedding day/programme/services.\n\n"
                    );
                Tbl_Advices.Inlines.Add(new Run("Before wedding 1 month\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
                Tbl_Advices.Inlines.Add(
      "o Last approval with the service providers and the hosting place.\n" +
      "o Make a seat-order.\n\n"
                    );
                Tbl_Advices.Inlines.Add(new Run("Before wedding 2 weeks\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
                Tbl_Advices.Inlines.Add(
      "o Last fitting on.\n" +
      "o Make sure every orders are fine.\n" +
      "o Hairdresser,makeup artist, nail artist: time fixing.\n\n"
                    );
                Tbl_Advices.Inlines.Add(new Run("Before wedding 1 week \n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
                Tbl_Advices.Inlines.Add(
      "o If it’s needed, coordinate with the host about the exact number and the number  of reserved rooms.\n" +
      "o One last time, talk over everyone’s personal tasks.\n" +
      "o Coordinate with services.\n\n"
                    );
                Tbl_Advices.Inlines.Add(new Run("1 day before the BIG DAY\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
                Tbl_Advices.Inlines.Add(
        "o Start the decorating, if it is managed by a serviceperson, show your exact wishes.\n" +
        "o Delivering the cakes.\n" +
        "o Pack everything for the day, that’s needed.\n\n"
                    );
                Tbl_Advices.Inlines.Add(new Run("Wedding day itself\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
                Tbl_Advices.Inlines.Add(
       "o Have time for everything, take care about slidings.\n" +
       "o Enjoy, it is only your day!\n\n");
                Tbl_Advices.Inlines.Add(new Run("After wedding\n\n") { FontWeight = FontWeights.Bold, FontSize = 24 });
                Tbl_Advices.Inlines.Add(
        "o Arrange the costs.\n" +
        "o Return all the borrowed objects.\n" +
        "o Dispatch of wedding gifts.");

            }

        }
    }
}
