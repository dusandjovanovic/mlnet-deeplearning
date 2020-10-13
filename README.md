# ML.NET

### Duboko učenje

**Duboko učenje** je podskup mašinskog učenja. Da bi se trenirali modeli dubokog učenja potrebne su velike količine podataka. Šabloni u podacima se predstavljaju velikim brojem slojeva. Odnosi izmedju podataka se kodiraju u vidu veza izmedju čvorova slojeva različitih težina. Što je jača veza jača je i težina. Kolektivno ovi entiteti i slojevi čine veštačke neuronske mreže - što više ima ovakvih slojeva mreža je dublja i odatle naziv *duboko učenje*.

**ML.NET** je kros-platformski *framework* za mašinsko učenje namenjen da bude korišćen u **.NET** okruženju. Neke od osnovnih mogućnosti su integracija modela mašinskog učenja u postojeće .NET sisteme, kros-platformski CLI i slično.

Ovaj framework proširava .NET sisteme mogućnostima mašinskog učenja, bez obzira da li se radi o online ili offline scenarijima. Na osnovu ovoga, mogu se praviti automatizovane predikcije na osnovnu podataka koji su dostupni aplikaciji. Aplikacije mašinskog učenja koriste šablone podataka kako bi pravile **predikcije** - nasuprot tome da budu programiranje unapred.

Centralni koncept ML.NET-a je **model mašinskog učenja**. Ovaj model predstavlja korake koje je potrebno primeniti kako bi se ulazni skup podataka transformisao u predikciju. Moguće je **istrenirati sopstveni model** ili **importovati postojeći** koji se rezultat neke druge tehnologije poput *TensorFlow-a*. Nakon što je model dostupan može se dodati aplikaciji koja će ga koristiti za predikcije.

Kao što je već navedeno, ovaj framework je kros-platformski i može se izvršavati na Windows-u, Linux-u i macOS-u oslanjajući se na .NET Core okruženje, ili pak samo Windows-u preko .NET orkuženja.

Evo i nekoliko primera predikcija:

|||
|-|-|
|Klasifikacija/kategorizacija|Podela feedback podataka u pozitivne i negativne kategorije|
|Regresija/Predikcija kontinualnih vrednosti|Predikcija cene nekretnina na osnovu lokacije|
|Detekcija anomalija|Detekcija nevalidnih bankovnih transakcija|
|Preporuke|Davanje preporuka korisnicima web platformi na osnovu njihovi prethodnih aktivnosti|
|**Klasifikacija slika**|Kategorizacija patologija u medicinskim izvorima slika|

## Model builder

**ML.NET Model Builder** je ekstenzija koja se koristi za gradjenje, treniranje i *deployment* modela mašinskog učenja. Oslanja se na koncept automatizovanog mašinskog učenja - *Automated machine learning (AutoML)* za istraživanje različitih algoritama i nalaženje optimalnog rešenja. Model Builder se u ovom obliku može koristiti na osnovu dataset-a i problema koji rešava i za rezultat daje generisani model.

**Scenariji** predstavljaju opis problema i tip predikcije koja se očekuje. Neki od očiglednijih primera bili bi klasifikacija raspoloženja odnosno *sentiment analysis* ili pak predikcija prodaje u nekoj kompaniji na osnovu istorijskih podataka prodaje.

1. **Klasifikacija teksta**, na primer, oslanja se na kategorizacije ulaznog teksta u jednu od više predefinisanih kategorija. Ova klasifikacija može biti binarna ili multi-klasna u zavisnosti od problema - takodje klasifikacija se može nadograditi tako da rešava neki konkretan problem poput detekcije spam tekstova ili ublažavanja rizika. 
2. **Predikcija vrednosti** oslanja se na regresiju - ova metoda może se nadogratiti tako da se koristi za predvidjanje prodaja, cena i slično.
3. **Klasifikacija slika** se koristi za identifikaciju slika različitih kateogrija, na primer različiti tipovi terena ili pak subjekta koji se nalaze na slikama poput životinja i ljudi. Scenario za klasifikaciju slika može se koristiti na osnovu više dostupnih slika i kategorija.
4. **Preporuke** u vidu *recommender sistema* se koriste za predikciju predloženig objekata nekom korisniku na osnovu njihovih prethodnih aktivnosti i odnosima sa drugim korisnicima/objektima. Na primer, kada postoji sistem sa korisnicima i objektima koje konzumiraju poput filmova ili knjiga, mogu se koristiti ovakvi sistemi za predikciju.

## Okruženje

Model mašinskog učenja može se **trenirati** na lokalnoj mašini ili cloud-u oslanjajući se naravno na *Microsoft Azure* tehnologiju. Treniranje na osnovu velikih dataset-ova moguće je samo u cloud-u s obzirom da su lokalne mašine jako ograničene procesorskim/memorijskim resursima. Lokalno treniranje može se koristiti za sve scenarije dok je treniranje klasifikacije slika jedino dostupno na cloud-u.

Nakon što se odabere scenario potrebno je dotaviti dataset u jednom od podržanih formata (`.tsv`, `.csv`, `.txt` ili `.jpg` i `.png` u slučaju klasifikacije slika). Dataset podataka se koristi za treniranje, evaluaciju i izbor najbolje modela za scenario.

### Izbor izlaza za predikciju

Dataset se može posmatrati kao tabela redova koji su primerci za testiranje - gde svaki od redova ima više atributa po kolonama. Svaki red ima **labelu** kao atribut koji treba predvideti i ostale **fature atribute**. Ovi preostali atributi koriste se za predikciju labele. Evo jednog [primera dataset-a](https://raw.githubusercontent.com/dotnet/machinelearning-samples/master/datasets/taxi-fare-train.csv) nad kojim se može ovo opisati. Ovo je primer podataka za predikciju cena taxi prevoza, feature atributi su trajanje vožnje taxijem i njena dužina.

### Treniranje modela

Treniranje je dugotrajan proces u kome se novi model koji se gradi uči kako da odgovara na pitanja - pitanja bazirana na scenariju nad kojim je gradjen. Nakon završenog treniranja model može da odgovara na pitanja i daje predikcije na osnovu do tada nevidjenih podataka. *AutoML* tehnologija koja se koristi istražuje više različitih modela kako bi našla onaj koji se najbolje pokazuje. Duže vreme treniranja dozvoljava bolje definisanje modela. 

| Veličina dataset-a        | 	Prosečno vreme treniranja          |
| ------------- |:-------------:|
| 0 - 10 MB      | 10 sekundi |
| 10 - 100 MB      | 10 minuta      |
| 100 - 500 MB | 30 minuta      |
| 500 - 1 GB | 60 minuta      |
| 1 GB+ | 180+ minuta      |

U tabeli su data okvirna vremena potrebna za treniranje modela. Medjutim, ovo dosta zavisi od tipa problema, broja feature atributa i naravno hardvera na kome se izvršava.

### Evaluacija modela

Evaluacija je proces procene kojim se ustanovljava koliko je dobar model. Model builder koristi izgradjeni model kako bi generisao predikcije na osnovu novih dataset-ova i samim tim mogu se doneti zaključci koliko su te predikcije precizne. Početni dataset se deli po razmeri 80-20 na podatke za treniranje i podatke za evaluaciju - gde se 20% podataka nakon treniranja koristi za evaluaciju generisanog modela.

Evaluacija na primeru klasifikacije sa više kategorija - osnovna metrika za evaluaciju je mikro-preciznost koja generalno opisuje koliko se puta dolazeća stavka ispravno klasifikovala. Śto je ova vrednost bliže jedinici predikcija je bolje evaulirana. Alternativa je makro-preciznost koja opisuje, za odredjenu kategoriju, koliko puta je bila dolazeća stavka ispravna.

Postoji više mogućnosti za poboljšanje nedovoljno dobrog modela poput dužeg vremena treniranja, proširavanja dataset-a ili pak balansiranja izvornih podataka. Poslednji pristup je posebno važan u slučaju rešavanja problema klasifikacije gde je važno da je broj stavki izbalansiran po kategorijama.

## ML.NET CLI

**ML.NET Command-line Interface** je alat koji se koristi za generisanje modela. Potrebno je dostaviti dataset podataka i problem mašinskog učenja koji treba rešiti. Ovako generisani model je serijalizovan i dodatni C# kod potreban za njegovo korišćenje je generisan.

Primer generisanja modela za klasifikaciju na osnovu dataset-a koji sadrži podatke sa servisa yelp.

`mlnet classification --dataset "yelp_labelled.txt" --label-col 1 --has-header false --train-time 10`

Izlaz koji se dobija sadrži više stavki:

* Serijalizovani `.zip` modela koji može da se koristi
* C# kod za pokretanje/ocenjivanje modela u .NET okruženju
* Log fajl koji sadrži korisne informacije u procesu izgradnje modela

Model i generisani C# kod mogu se direktno koristiti u .NET okruženju i framework-u poput ASP.NET Core bez obzira da li se radi o web ili desktop aplikacijama. Na osnovu loga se može vršiti ručno testiranje ili ispitivanje koji su algoritmi izabrani u procesu treniranja modela.

![alt text](https://docs.microsoft.com/en-us/dotnet/machine-learning/media/automate-training-with-cli/cli-multiclass-classification-metrics.png "CLI metrics")

Kao što je prethodno pomenuto, poslednih 20% dataset-a koristiće se za evaluaciju modela. U slučaju modela za klasifikaciju svi parametri koji ga opisuju poput mirko/makro preciznosti to i opisuju.

## ML.NET i osnovni arhitekturni principi

Aplikativna struktura se svodi na iterativni proces razvoja modela koji se sastoji iz više koraka:

* Skupljanje i učitavanje podataka `IDataView` objekat
* Specifikacija `pipleine-a` operacija ekstrakcije podatka (feature atributa) i aplikacije algoritama mašinskog učenja
* Treniranje modela pozivom metode `Fit()` nad pipeline-om
* Evaluacije modela i poboljšanja kroz više iteracija
* Čuvanje modela u binarnom formatu
* Učitavanje modela nazad u `ITransformer` objekat
* Formiranje predikcija pozivom `CreatePredictionEngine.Predict()`

```cs
static void Main(string[] args)
       {
           MLContext mlContext = new MLContext();

           // 1. Ulazni podaci
           InputData[] inputData = {
               new InputData() { ... },
               new InputData() { ... },
               new InputData() { ... },
               new InputData() { ... } };
               
           IDataView trainingData = mlContext.Data.LoadFromEnumerable(inputData);

           // 2. Specifikacija pipeline-a
           var pipeline = mlContext.Transforms.Concatenate("Features", new[] { /* feature atributi ulaznih podataka */  })
               .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: /* label atribut za predikciju */, maximumNumberOfIterations: 100));

           // 3. Treniranje
           var model = pipeline.Fit(trainingData);

           // 4. Predikcija na osnovu modela
           var size = new InputData() { ... };
           var labelAttribute = mlContext.Model.CreatePredictionEngine<InputData, Prediction>(model).Predict(size);

           // labelAttribute promenljiva sadrži predikciju na osnovu feature atributa
       }
```

U ovom pokaznom kodu može se videti najosnovniji primer učitavnja podataka, definisanja pipeline-a, treniranja i naravno korišćenja predikcija.

### Model, pripremanje podataka i evaluacija

U osnovi, generisani model je objekat koji sadrži **transformacije** koje se primenjuju na ulaznim podacima kako bi se **došlo do predikcija**.  Na primeru klasifikacije, kompleksan model bi ulaznu deskripciju razbio na skup feature atributa, uklonio suvišne reči i zatim prebrojio reči u tekstu. Nakon kombinovanja i normalizacije, model bi izvršio multiplikaciju i preveo nadjene reči u težinski presek. Na osnovu ovako generisanog težinskog preseka se vrši preraspodela i dodeljivanje kategoriji - na osnovu prethodnih poklapanja. Postoji više tipova modela poput **linearnih**, **decision tree modela** ili **generalizovanih aditivnih modela**. O ovome će više reči biti kasnije.

**Pripremanje podataka** je potrebno kada su podaci dostupni aplikaciji u obliku koji nije pogodan za mašinsko učenje. Pre nego što je moguće koristiti podatke kako bi se vadili potrebni parametri modelu, neophodno je sirove podatke pre-procesirati. Više pristupa poput promene formata, normalizacije, proširivanja dimenzija ili pak uklanjanja redudanse su ovde primenljivi. U zavisnosti od scenarija i problema koji se rešava treba drugačije pristupiti ovom koraku. Sve u svemu, potrebno je modelu dostaviti podatke u njemu odgovarajućem i obradivom obliku - ovo ćesto nije slučaj sa *sirovim podacima* i potrebno ih je obraditi.

**Evaluacija modela** se svodi, kao što je već pomenuto, na posmatranja opsinih atributa modela koji se dobijaju prilikom korišćenja novih podataka (podataka koji prethodno nisu bili poznati modelu). Svaki tip zadataka mašinskig učenja ima odredjene metrike koje se koriste za evaluaciju preciznosti modela u procesu generisanja predikcija. 

```cs
InputData[] testInputData =
        {
            new InputData() { ... },
            new InputData() { ... },
            new InputData() { ... },
            new InputData() { ... }
        };

var testInputDataView = mlContext.Data.LoadFromEnumerable(testInputData);
var testLabelDataView = model.Transform(testInputDataView);

var metrics = mlContext.Regression.Evaluate(testLabelDataView, labelColumnName: /* label atribut */);
```

U ovom pokaznom kodu može se videti kako se evaluira regresivni zadatak nad novim test dataset-om. Što je greška manja - postoji bolja korelacija izmedju predvidjenog izlaza i očekivanog.

### ML.NET arhitektura

Svaka ML.NET aplikacija polazi od jednog `MLContext` objekta - singleton objekta koji sadrži kataloge. Katalog je fabrika iliti *factory* za učitavanje i čuvanje podataka, transformacije, treniranje i komponente operacija. 
* `DataOperationsCatalog` se koristi za učitavanje i snimanje podataka.
* `TransformsCatalog` se koristi za pripremanje podataka.
* `MulticlassClassificationCatalog` za više-klasnu klasifikaciju, postoji još mnogo kataloga za dodatne scenarije..
** `AnomalyDetectionCatalog`, `RegressionCatalog` i drugi.
* `ModelOperationsCatalog` za korišćenje modela.

#### Izgradjivanje pipeline-a

```cs
var pipeline = mlContext.Transforms.Concatenate("Features", new[] { /* feature atribut(i) */ })
       .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: /* label atribut */, maximumNumberOfIterations: 100));
```

Svaki od kataloga za različite scenarije sadrži više proširenih metoda. Iz primera se može videti da su `Concatenate` i `Sdca` ovakve metode. Svaka od kataloških metoda pridodaje `IEstimator` objekat pipeline-u. **`Estimator` objekat** je netrenirani transformator i potrebno ga je "uklopiti" sa podacima da bi se dobio pravi transformator. **Procesom treniranja** i pozivom metode `Fit()` dobija se kao rezultat **transformator** koji se kasnije koristi za predikcije.

```cs
var model = pipeline.Fit(trainingData);

...

IDataView predictions = model.Transform(inputData);
```

Nakon što se dodaju potreni objekti pipeline-u vrši se testiranje modela. Pozivom metode `Fit()` koriste se ulazni podaci za testiranje kako bi se došlo do parametara modela. Rezultujući model implementira `ITransformer` interfejs - u ovom obliku je u mogućnosti da **transformiše ulazne podatke u predikcije**.

### Predikcije modela, data modeli i scheme

Ulazni podaci se mogu transformisati u predikcije po principu više od jednom iliti *bulk* ili jedan po jedan. `CreatePredictionEngine()` metoda uzima ulaznu i izlaznu klasu. Imena atributa odredjuju imena kolona podataka koja se koriste u procesu treniranja i predikcije.

```cs
var size = new InputData() { Size = 2.5F };
var predEngine = mlContext.CreatePredictionEngine<InputData, Prediction>(model);
var featureAttr = predEngine.Predict(size);
```

U jezgru ML.NET-a pipeline-a za mašinsko učenje stoje `DataView` objekti. Svaka transformacija pipeline-a poseduje `input schemu` (imena podataka, veličine i tipovi očekivani na ulazu transformatora) i `output schemu` (isto ali koje se dobija na izlazu transformatora). Svaki od ovih objekata poseduje kolone i redove gde svaka kolona ima jedinstveno ime i njoj prispojen tip podataka.

Svaki algoritam traži ulaznu kolonu koja je vektor - po konvenciji nazvana *Features* jer opisuje takozvane feature atribute. U kodu ispod će, na primer, kolona `Xyz` biti ovako tretirana.

```cs
var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Xyz" })
```

Svi algoritmi takodje formiraju nove kolone nakon što završe sa obradom podataka u svom mestu pipeline-a i daju predikciju. U slučaju regresije, na primer, to će biti kolona *score*. Bitna stavka `DataView` objekata je što se izvršavaju "lenje" - oni se učitavaju i evaluiraju jedino u procesu treniranja i evaluacije, kao i predikcije.

## Zadaci mašinskog učenja u ML.NET-u

Zadatak u ML.NET-u je **tip predikcije ili zaključivanja**, koji se zasniva na problemu ili nekom pitanju i podacima. Na primer, zadatak klasifikacije dodeljuje kategoriju dostupnim ulaznim podacima. Ovi zadaci se oslanjaju na šablone u podacima i nisu eksplicitno programirani unapred.

Postoje **supervizovani i nesupervizovani zadaci**. Supervizovani se koriste za predikciju labela do tada nevidjenih podataka. Nesupervizovani, sa druge strane, nalaze strukturu i strukturne zavisnosti u podacima i često se koriste za nalaženje pod-grupa ulaznih podataka.

### Binarna klasifikacija

Za najbolje rezultate potrebno je da ulazni podaci budu balansirani po kategorijama. Vrednosti koje nedostaju treba da se odstrane pre treniranja a kolona ulaznog atributa labele mora biti *boolean* tipa. Rezultat je `PredictedLabel` vrednost koja je bazirana na *score-u* dobijenog na osnovu algoritama. Postoji mnogo algoritama za ovu klasifikaciju a neki od primera su `FastForestBinaryTrainer`, `GamBinaryTrainer` i `FieldAwareFactorizationMachineTrainer`.

Evo i kratkog objašnjena prvog algoritma:

> Decision trees are non-parametric models that perform a sequence of simple tests on inputs. This decision procedure maps them to outputs found in the training dataset whose inputs were similar to the instance being processed. A decision is made at each node of the binary tree data structure based on a measure of similarity that maps each instance recursively through the branches of the tree until the appropriate leaf node is reached and the output decision returned.

### Više-klasna klasifikacija

Ovo je tip supervizovanog zadatka koji predvidja klasu ulaznih podataka, slično kao i prethodni s tim što postoji više klasa. Izlaz ovog algoritma je takozvani *classifier* koji se koristi za predvidjanje klasa - na osnovu polaznog dataset-a za treniranje. Ovakav model može se trenirati preko više algoritama poput `LightGbmMulticlassTrainer`
, `SdcaMaximumEntropyMulticlassTrainer` ili `SdcaNonCalibratedMulticlassTrainer`.

### Regresija

Još jedan supervizovan zadatak koji se koristi za predvidjanje vrednosti labele na osnovu skupa srodnih atributa. Labela može biti bilo koje kontinualne vrednosti i nije iz ograničenog skupa vrednosti. Algoritmi regresije mdeluju odnose labele prema njenim srodnim atributima i donose zaključke kako će se menjati sa promenom ovih posmatranih vrednosti. Ulazni skup je sačinjen od poznatih labela i njehovi ustanovljenih vrednosti. Izlaz je funckija koja može da se koristi za predikciju vrednosti na osnovu bilo kog skupa atributa.

### Klasterovanje

Ovo je primer nesupervizovanog zadataka koji grupiše instance podatka u klastere koji sadrže slične vrednosti. Klasteri se mogu koristiti za izvlačenje zavisnosti koje se ne primećuju običnim obilascima svih ulaznih podataka. Ulazni/izlazni skupovi zavise od izabrane metodologije grupisanja. Više pristupa poput distribucije ili povezanosti mogu biti iskorišćeni.

### Nalaženje anomalija

Ovaj zadatak pravi model za pronalaženje anomalija u ulaznom skupu podataka bazirani na principu *Principal Component Analysis (PCA)*. PCA se često koristi zato što otkriva unutrašnju strukturu podataka i objašnjava varijacije istih. Analizira ulazne podatke, traži korelacije izmedju njih i teži u nalaženju kombinacija podataka koje najbolje opisuju razlike u mogućim izlazima. Slično kao u algoritmima klasifikacije, postoji rezultat `PredictedLabel` koji opisuje da li je ulazna vrednost anomalija ili ne.

### Rangiranje

Zadatak rangiranja formira takozvani *ranker* na osnovu ulaznog skupa podataka. Ulazni set podataka treba da sadrži podatke rangirane po nekom kritetijumu - labele rangova su najčešće celi brojevi. Ranker je treniran tako da može da zaljuči rang novih podataka na osnovu prethodnih.

### Recommender sistemi

Zadatak koji preporučuje listu objekata na osnovu ulaznih parametara subjekta. ML.NET koristi proces poznat kao *Matrix factorization (MF)*, kolaborativni filtering algoritam za preporuke kada postoje istorijski podaci na osnovu kojih se može koristiti.

### Izbor algoritama?

Za svaki od prethodno opisanih zadataka postoji mnoštvo algoritama koji se mogu izabrati. Izbor itekako zavisi od problema za koji je potrebno dobiti predikcije, karakteristika ulaznih podataka i naravno hardvera. Kako je treniranje modela itertivan proces očekuje se isprobavanje više algoritama.

Algoritmi se baziraju na takozvanim ***feature-ima*** a ovo su numeričke vrednosti dobijene od ulaznih podataka. 

Feature vrednosti se dobijaju jednim od dostupnih **transformacija ulaznih podataka**.
* Pripremanje ulaznih podataka za mašinsko učenje zahteva ove transformacije. Svaka transformacija očekuje i proizvodi podatke u odredjenom obliku, koji se navode u ulančanoj specifikaciji:
1. Mapiranje i grupisanje kolona
2. Normalizacija i skaliranje
3. Konverzije izmedju tipova
4. Transformacije teksta/slika poput **izvlačenja piksela**
5. **Transformacije deep learning modela** poput `LoadTensorFlowModel` i druge..

Algoritam se svodi na matematiku koja se primenjuje kako bi se napravio model. Različiti modeli formiraju modele različitih karakteristika. U ML.NET-u isti algoritam može da se primeni na više različitih zadataka ali će izlaz biti drugačiji. Za svaku kombinaciju postoji komponenta koja izvršava algoritam i interpretaciju. Na primer, `SdcaRegressionTrainer` koristi `StochasticDualCoordinatedAscent` algoritam primenjen na `Regression` zadatak.

**Linearni algoritmi** daju modele koji računaju *score* na osnovu linearne kombinacije ulaznih podataka i **skupa težina**. Upravo skup težina se formira u procesu treniranja modela. Pre treniranja neophodna je normalizacija. Ovi algoritmi prolaze više puta kroz trening dataset-ove i formiraju model.

| Algoritam      | 	Osobine          |
| ------------- |:-------------:|
| Averaged perceptron     | Najbolji za klasifikaciju teksta |
| Stochastic dual coordinated ascent      | Dobre performanse bez podešavanja      |
| L-BFGS | Dobar u slučaju velikog broja faeture-a     |
| Symbolic stochastic gradient descent | Najbrži algoritam za binarnu klasifikaciju     |

**Decision tree algoritmi**

Formiraju model koji poseduje više odluka poput *flow chart-a*. Nije neophodna normalizacija, niti linearna separacija feature-a. Ovi algoritmi zahtevaju više resursa i nisu skalabilni poput linearnih. Dobro se pokazuju nad dataset-ovima koji mogu da stanu u radnu memoriju. 

| Algoritam      | 	Osobine          |
| ------------- |:-------------:|
| Light gradient boosted machine     | Najbolji za binarnu klasifikaciju |
| Fast tree      | Dobar u slučaju obrade slika      |
| Fast forest | Dobar u slučaju podataka sa šumovima     |
| Generalized additive model (GAM) | Dobar izbor u slučaju problema koji se lako rešavaju stablima     |

Ovo su dva najznačajnija i najčešće korišćenija tipa algoritama. Postoje i drugi koji neće biti navedeni.

### Metrike algoritama i evaluacija

Svaki od algoritama generiše zasebni model koji je po suštini i strukturi poseban. Evaluacija ovako generisanih modela je različita od tipa modela i specifična je za zadatak mašinskog učenja koji se izvršava. Na primer, u slučaju klasifikacije, validacija se dobija procenom koliko dobro se predvidjena klasa poklapa sa stvarnom. U slučaju klasterovanja, sa drug strane, evaluacija uzima u obzir koliko su blizu klasterovane pod-grupe.

Prema tome, za svaki tip zadataka/algoritama postoje posebne metrike koje se koriste u procesu evaluacije. **Evaluacija** se može posmatrati itertivno i model se može poboljšavati kroz više koraka ali je neophodno **posmatrati specifične metrike po tipu modela**.

Neki od načina poboljšanja modela:
* Promena strukture problema, postavljanje novih pitanja
* Pribavljanje dodatnih primeraka podataka
* Dodavanje konteksta podacima
* Korišćenje smislenih podataka
* Cross-validacija (podela dataseta i primena različitih algoritama nad grupama podataka) ili pak izbor drugog algoritma

# Implementacija - Transfer learning i klasifikacija slika

Ima više tipova neuronskih mreža a najčešće su *Multi-Layered Perceptron (MLP)*, *Convolutional Neural Network (CNN)* i *Recurrent Neural Network (RNN)*. MLP je naprostiji vid neuronske mreže koji mapira niz ulaza na niz izlaza - dobar je izbor kada podaci nemaju prostornu ili vremensku komponentu. CNN iskorišćava *convutational* slojeve za procesiranje prostornih informacija. Dobar izbor predstavljaju u slučaju obrade slika - pogotovo prepoznavanja regije unutar slike. RNN mreže, na kraju, dozvoljavaju perzistenicju stanja ili memorije koja će se koristiti kao ulazna. Koriste se u slučaju analiza vremenskih serija podataka ukoliko je sekvencna uredjenost važna.

### Dataset

Korišćeni dataset za treniranje zove se _Intel Image Classification - Image Scene Classification of Multiclass_. Sadrži podatke za treniranje `seg_train` kao i skup podataka za evaluaciju modela u `seg_pred`. Ideja je napraviti **klasifikacioni model za prepoznavanje scena slika** ~ oblikovan po ovom datasetu podataka. Dakle, za svaku od kategorija u datasetu treba da postoji kategorija predikcije u novom modelu.

    dataset
        |
        ->seg_train
        |  |
        |  ->buildings  -> (1.jpg, 2.jpg, ...)
        |  |   
        |  ->glacier
        |  |   
        |  ->sea
        |  |   
        |  ->forest
        |  |   
        |  ->street
        |  |   
        |  ->mountains
        |
        ->seg_test
        |  
        |
        ->seg_pred


Treniranje **deep learning modela** za klasifikaciju slika u one koje sadrže pukotine i one koje ih nemaju. Koristi se tehnika *transfer learning* i kao osnova već trenirani *TensorFlow* model. Za evaluaciju koristi se slika i posmatra predvidjena klasa. Korišćen je *Image Classification API* koji daje pristup *TensorFlow C++ API-u*.

Treniranje polazi od već treniranog modela koji se koristi i pravi se nadgradnja koja rešava problem pomenute kategorizacije. Treniranje ima dve faze - prva je nad *zaledjenim slojevima* postojećeg modela (svi slojevi do penultimate sloja) i ovde se vrednosti samo propuštaju. Šabloni ovih slojeva se bave računicom koje prave razliku izmedju osnovnih klasa slika. Druga faza je faza pravog treniranja gde se refinira poslednji sloj mreže - iterativna je i uzima u obzir gubitke preciznosti kako bi model bio što bolji. Polazni model (101-slojna varijanta Rezidualnog mrežnog (ResNet) v2 modela) kategorizuje sliku u više hiljada kateogirja i za ulaznu sliku veličine 224 x 224px daje verovatnoće pripadanja svakoj kategoriji. Deo ovog modela se koristi za treniranje novog modela kako bi davao predikcije izmedju novih klasa.

Novi model će biti u stanju da kategorizuje sliku u **više različitih kategorija**. Dataset koji se koristi za treniranje **podeljen je po direktorijumima** gde slike **iste klase pripadaju istom folderu**. Treba ovo uzeti u obzir prilikom pisanja pipeline-a i naznačiti modelu. Rezultat ovoga je model koji je sposoban da se adaptira na datasetove formirane u ovom obliku. U ovom slučaju to su **kategorije glacier, sea, forest, street, mountains i buildings.**

Klasa `ImageData` koristi se da opiše šemu ulaznih podataka. 

Klasa `ModelInput` koristi se za ulazne podatke kojima se hrani model i treba da sadrže **byte-reprezentaciju slika**. 

Klasa `ModelOutput` sadrži izlaz iz modela uključujući i **predikciju klase**.

Klasa `Classification` sadrži sve važne metode za treniranje modela i kasniju predikciju. Treba pre svega napraviti *chain* transformacija gde će se labela konvertovati u numeričku vrednost a zatim i primeniti transformacija `LoadRawImageBytes`. Pozivom metoda `Fit` i `Transform` dobija se uskladjeni skup podataka od dataseta za treniranje.

Počinje se učitavanjem svih podataka iz dataseta:

```cs
/**
* MLContext klasa je polazna tacka inicijalizacije
* kreira novo ML.NET okruzenje i perzistira model koji se trenira
*/
mlContext = new MLContext();

/**
* Podaci se ucitavaju redosledom kojim su u direktorijumima
* da bi se balansirao ulaz treba odradi shuffling nad njima
*/
IEnumerable<ImageData> images = LoadImagesFromDirectory(folder: assetsRelativePath, useFolderNameAsLabel: true);
IDataView imageData = mlContext.Data.LoadFromEnumerable(images);
IDataView shuffledData = mlContext.Data.ShuffleRows(imageData);

```
Neophodno je učitati same slike i izvršiti konverziju labela u numeričke vrednosti. Za treniranje su potrebna dva pod-seta - jedan za treniranje i drugi za validaciju koji se dobijaju po odnosu 70/30. Subset za validaciju se zatim dalje deli po odnosu 90/10 gde se veći deo ulaza koristi za treniranje a manji za validaciju.

```cs
/**
* Model ocekuje da ulazi budu u numerickom obliku -> LabelAsKey
* EstimatorChain se pravi od MapValueToKey i LoadRawImageBytes transofrmacija
*/
var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
      inputColumnName: "Label",
      outputColumnName: "LabelAsKey")
  .Append(mlContext.Transforms.LoadRawImageBytes(
      outputColumnName: "Image",
      imageFolder: assetsRelativePath,
      inputColumnName: "ImagePath"));

...

 /**
*  Kvalitet dobijenih procena se meri na osnovu validacionog podskupa
*  Pred-procesirani podaci se prema tome dele
*  70% se koristi za treniranje
*  30% za validaciju
*/
TrainTestData trainSplit = mlContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.3);
TrainTestData validationTestSplit = mlContext.Data.TrainTestSplit(trainSplit.TestSet);
```

Treniranje se sastoji iz par koraka, prvo se *Image Classification API* koristi za treniranje a zatim se enkodirane labele prevode u izvorne kategoričke vrednosti. **Pipeline treniranja** sadrži `mapLabelEstimator` i `ImageClassificationTrainer`.

```cs
var trainingPipeline = mlContext.MulticlassClassification.Trainers.ImageClassification(classifierOptions)
    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
    
ITransformer trainedModel = trainingPipeline.Fit(trainSet);
```

Metode `ClassifySingleImage` i `ClassifyExternalImage` se zatim koriste za klasifikaciju nad kontekstom izgradjenog modela. Prva metoda se koristi interno nakon treniranja i zove se za deo dataseta a druga se koristi na zahtev kada se sa Front-End-a aplikacije šalje slika na klasifikaciju.

### Korišćenje modela - klasifikacija

Potrebno je koristiti convenience API nazvan *Prediction Engine* koji dozvoljava izvršenje predikcije nad jednom instancom podataka, potrebno je pre toga svesti `IDataView` na odgovarajući model objekat. Svaka predikcija se ispisuje u konzoli.

```cs
PredictionEngine<ModelInput, ModelOutput> predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);

ModelInput image = mlContext.Data.CreateEnumerable<ModelInput>(data, reuseRowObject: true).First();
ModelOutput prediction = predictionEngine.Predict(image);
```

# Implementacija - Detekcija objekata

Detekcija objekata primenjuje klasifikaciju slika na granularniji način tako što locira a zatim i kategoriše entitete (objekte) na slikama. Detekcija objekata je problem koji se rešava CNN mrežama. Model koji se koristio u ovu svru je **Tiny YOLOv2 model**. Ovaj model je treniran i sastoji se od 15 slojeva koji mogu da predvide 20 različitih klasa objekata. Strukture podataka koje opisuju ulaze/izlaze ovog modela nazivaju se *tenzori*. Mogu se posmatrati kao kontejneri koji čuvaju podatke u N-dimenzija. Ulazni sloj ovog modela zove se `image` i očekuje tenzor dimenzija `3 x 416 x 416` dok je izlazni sloj nazvan `grid` i generiše tenzor dimenzija `125 x 13 x 13`.

Ovaj model dakle uzima sliku u obliku `3(RGB) x 416px x 416px`. Slojevi razbijaju i procesiraju sliku da bi se na kraju dobio izlaz u obliku `13 x 13` grid-a gde se svaka ćelija u gridu sastoji od 125 vrednosti.

*The Open Neural Network Exchange (ONNX)* je open-source format modela što dozvoljava treniranje pod jednim okvirom i konzumiranje pod drugim. Model koji se koristi je upravo u ONNX formatu, tačnije serijalizovani oblik slojeva i naučenih šablona. *ImageAnalytics* paket ML.NET-a sadrži pomagala kojima sa slika svodi na numeričke vrednosti koje se zatim koriste u pipeline-ovima.

Klasa `ImageNetData` je ulazna klasa podataka, važno je da sadrži labelu svakog podatka kao i apsolutne putanje svih onih podataka koji se obradjuju.

Klasa `ImageNetPrediction` je klasa za predikcije i ima niz numeričkih vrednosti koji sadrži dimenizje, score objekata kao i verovatnoće klasa za svaki *bounding box* detektovan na slici.

Neophodna je parser klasa za post-procesiranje izlaza modela. Model segmentira sliku na `13 x 13` grid gde je svaka grid ćelija veličine `32px x 32px`. Svaka ćelija ima 5 mogućih bounding box objekata gde se svaki od njih sastoji od 25 elemenata (x, y, w, h, o i p1-p20 verovatnoća za svaku klasu). Izlaz modela je niz dużine 21125 koji opisuje tenzor dimenzija `125 x 13 x 13`. Potrebno je svesti ovaj niz na oblik tenzora.

Klasa `DimensionsBase` sadrži x, y, visinu i širinu objekta koji opisuje.

Klasa `BoundingBoxDimensions` je nasledjuje i služi za opis osobina bounding box-a.

Klasa `BoundingBox` sadrži dimenzije bounding box-a u obliku prethodno pomenute klase, labelu koja opisuje klasu objekta, confidence atribut koji govori o sigurnosti predikcije kao i pravougaonu reprezentaciju box-a i njegovu boju (jer svaka klasa ima jedinstvenu boju).

Klasa `OutputParser` treba da obezbedi prethodno naznačeno parsovanje izlaza modela. Kao što je već napomenuto - `13 x 13 grid :: 32px x 32px` gde svaka ćelija uokviru sebe ima 5 bounding box-va koji sadrže po 25(5+20) feature atributa. Prema tome, svaka ćelija ima 125 informacija.

Ova klasa definiše temena bounding box-ova u vidu odnosa visina/širina. Prilikom proračuna traži se pomeraj od podrazumevanih vrednosti. Takodje treba definisati koje će klase model da predvidi kao i boje uparene sa svakom od klasa. Neke od važnih pomoćnih funkcija su `GetOffset` za mapiranje elemenata id jednodimenzionalnog izlaza na poziciju u tenzoru. `ExtractBoundingBoxes` za izvlačenje dimenzija box-ova na osnovu prethodne metode. `GetConfidence` za izvlačenje confidence vrednosti. `ExtractClasses` za izvlačenje klasnih predikcija box-ova, ponovo oslanjajući se na `GetOffset` metodu i druge. Na kraju, koristi se metoda `FilterBoundingBoxes` za filtriranje box-ova koji se preklapaju.

```cs
/**
* Svaka slika se deli u grid 13 x 13 celija. Svaka celija ima 5 bounding box-ova
* Ovde se procesuiraju svi box-ovi svake celije
*/
for (int row = 0; row < ROW_COUNT; row++)
  for (int column = 0; column < COL_COUNT; column++)
      for (int box = 0; box < BOXES_PER_CELL; box++)
      {
          /**
           * izracunati polaznu tacku box-a u odnosu na jednodimenzinalni ulaz
           */
          var channel = (box * (CLASS_COUNT + BOX_INFO_FEATURE_COUNT));

          /**
           * ExtractBoundingBoxDimensions za nalazenje dimenzija box-a
           */
          BoundingBoxDimensions boundingBoxDimensions = ExtractBoundingBoxDimensions(yoloModelOutputs, row, column, channel);

          ...

          /**
           * Ako bounding box prevazilazi 'threshold' napraviti novi i dodati ga u listu box-ova
           */
          boxes.Add(new BoundingBox(){ Dimensions = new BoundingBoxDimensions, ...}};
```

### Korišćenje modela

**Eksterno učitana slika** može se predati modulu za detekciju objekata metodom `ProcessExternalImage` nad detection instancama.

```cs
/**
* Potrebna je instanca OnnxModelScorer-a kako bi se ocenio ulaz
*/
modelScorer = new OnnxModelScorer(modelFilePath, mlContext);
IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);

OutputParser parser = new OutputParser();
var boundingBoxes =
 probabilities
 .Select(probability => parser.ParseOutputs(probability))
 .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));

IList<BoundingBox> detectedObjects = boundingBoxes.ElementAt(0);
LogDetectedObjects(selectedImagePath, detectedObjects);
Bitmap bitmap = BitmapWithBoundingBox(selectedImagePath, detectedObjects);
```

`BitmapWithBoundingBox` metoda iscrtava bounding box-ove oko predikcija objekata. Svaki objekat će biti uokviren i imaće labelu koja naznačava njegovu klasu.

Pipeline mora da poznaje šemu podataka sa kojima radi. Ovde će se koristiti pipeline od četiri transformacije.

```cs
/**
* Pipeline ima 4 transformacije:
*  LoadImages za ucitavanje Bitmap slike
*  ResizeImages za reskaliranje ucitane slike (ovde je to 416 x 416)
*  ExtractPixels menja reprezentaciju piksela u numericki vektor
*  ApplyOnnxModel ucitava ONNX model i koristi ga za score-ing
*/
var pipeline = mlContext.Transforms.LoadImages(outputColumnName: "image", imageFolder: "", inputColumnName: nameof(ImageNetData.ImagePath))
              .Append(mlContext.Transforms.ResizeImages(outputColumnName: "image", imageWidth: ImageNetSettings.imageWidth, imageHeight: ImageNetSettings.imageHeight, inputColumnName: "image"))
              .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "image"))
              .Append(mlContext.Transforms.ApplyOnnxModel(modelFile: modelLocation, outputColumnNames: new[] { TinyYoloModelSettings.ModelOutput }, inputColumnNames: new[] { TinyYoloModelSettings.ModelInput }));
```

Izlaz iz modela se prosledjuje parser klasi kako bi se dobile prave vrednosti verovatnoća za svaki bounding box. Na kraju, vizualizacija se svodi na praćenje svih box-ova i iscrtavanja pravougaonika oko detektovanih objekata.

```cs
IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);

OutputParser parser = new OutputParser();
var boundingBoxes =
  probabilities
  .Select(probability => parser.ParseOutputs(probability))
  .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));
```

# Implementacija - Arhitektura

Sve klase su prethodno objašnjene, evo kratkog pregleda rasporeda i organizacije. Moduli za klasifikaciju i detekciju imaju odvojene `namespace-ove` - `MLNet.classification` i `MLNet.detection`. Sve pomoćne klase se nalaze u ovim modulima.

```
/
  App.xaml
  MainWindow.xaml
  AssemlyInfo.cs
  classification/
    workspace/ -> radni direktorijum koji sadrži .pb fajl modela i kešira detalje klasifikacije
    Classification.cs
    ImageData.cs
    ImageDataInMemory.cs
    ModelInput.cs
    ModelOutput.cs
    ...
  detection/
    model/
      TinyYolo2_model.onnx
    BoundingBox.cs
    Detection.cs -> glavna klasa za detekciju objekata
    DimensionsBase.cs
    ImageNetData.cs
    ImageNetPrediction.cs
    OnnxModelScorer.cs
    OutputParser.cs
    ...
  common/
    ImageUtils.cs
    NativeConsole.cs
    NativeMethods.cs
  dataset/
    seg_pred/
    seg_test/
    seg_train -> dataset podaci za testiranje
```

Instance klasa se inicijalizuju samo jednom i kasnije koriste u projektu.

```cs
/**
* Instanca klase za klasifikaciju
*/
private Classification classification = new Classification();

/**
* Instanca klase za detekciju objekata
*/
private Detection detection = new Detection();

/**
* Override klasa konzolnog ulaza
*/
private NativeConsole nativeConsole;
```

Neophodno je nakon renderovanja UI elemenata prozora pripremiti obe instance. U slučaju klasifikacije to je treniranje (ili učitavanje keširanog modela ako je već treniran u prethodnoj sesiji), u slučaju detekcije objekata to je učitavanje treniranog modela. Obe klase poseduju metodu `.Process()` kojom se ovo i dobija.

```cs
private void Initiate(object state)
{
       classification.Process();
       detection.Process();
       Console.Out.Flush();
}
```

Nakon ovoga mogu se koristiti ostale metode klasa poput `.ClassifyExternalImage(Bitmap)` za klasifikaciju učitanih slika ili pak `.ProcessExternalImage(string)` za detekciju objekata. Obe metode pripadaju odvojenim instancama za klasifikaciju/detekciju respektivno.

# Prototip projekat

Projekat demostrira dve funkcionalnosti čije su implementacije prethodno opisane:
* **Klasifikaciju slika** u vidu prepoznavanja scene slike
* **Detektciju objekata** i označavanje istih na slici (zajedno sa klasnim atributom objekta)

Prilikom pokretanja programa prvo je neophodno **trenirati model podacima** iz dataseta (koji je importovan u sklopu projekta) - ili nakon prvog pokretanja učitati keširane vrednsoti za predikciju. U ovom slučaju, po datasetu za treniranje, model može da prepozna nekoliko kategorija a to su **kategorije glacier, sea, forest, street, mountains i buildings.**

**Na desnoj strani programa postoji konzolni prikaz napredovanja treniranja kao i izlaz svih funkcija koje će se kasnije koristiti.** U outputu koji sledi je prikazan proces treniranja na osnovu dataseta koji prolazi kroz dve faze - **Bottlneck** i **Training**.

Na kraju se odvaja mali deo dataseta za validaciju i mogu se videti rezultati poredjenja u izlaznoj konzoli na desnoj strani (ovaj deo dataseta se nije koristio u procesu treniranja).

`Image: 2615.jpg | Actual Value: mountain | Predicted Value: mountain` na primer predstavlja tačnu predikciju.

```
Loading and shuffling dataset..
Splitting dataset to train/vaild subsets..

Beginning to train the model..
Saver not created because there are no variables in the graph to restore
Phase: Bottleneck Computation, Dataset used: Validation, Image Index:   1
Phase: Bottleneck Computation, Dataset used: Validation, Image Index:   2
Phase: Bottleneck Computation, Dataset used: Validation, Image Index:   3
Phase: Bottleneck Computation, Dataset used: Validation, Image Index:   4

…

Phase: Bottleneck Computation, Dataset used: Validation, Image Index: 764
Phase: Bottleneck Computation, Dataset used: Validation, Image Index: 765
Phase: Bottleneck Computation, Dataset used: Validation, Image Index: 766

...

Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   6
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   7
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   8

…

Phase: Bottleneck Computation, Dataset used:      Train, Image Index: 1949
Phase: Bottleneck Computation, Dataset used:      Train, Image Index: 1950
Phase: Bottleneck Computation, Dataset used:      Train, Image Index: 1951

...

Epoch:  34, Accuracy: 0.91282076, Cross-Entropy: 0.28586656
Phase: Training, Dataset used: Validation, Batch Processed Count:  78, Epoch:  35, Accuracy: 0.91282076, Cross-Entropy: 0.28623667
Phase: Training, Dataset used: Validation, Batch Processed Count:  78, Epoch:  36, Accuracy: 0.91282076, Cross-Entropy: 0.28626505
Phase: Training, Dataset used: Validation, Batch Processed Count:  78, Epoch:  37, Accuracy: 0.91282076, Cross-Entropy: 0.28660822
Saver not created because there are no variables in the graph to restore
Restoring parameters from C:\Users\Dusan\Desktop\MLNet\classification/workspace\custom_retrained_model_based_on_resnet_v2_101_299.meta
Froze 2 variables.
Converted 2 variables to const ops.

Classifying multiple images..
Image: 2831.jpg | Actual Value: forest | Predicted Value: forest
Image: 1964.jpg | Actual Value: buildings | Predicted Value: buildings
Image: 2496.jpg | Actual Value: forest | Predicted Value: forest
Image: 1947.jpg | Actual Value: glacier | Predicted Value: glacier
Image: 1432.jpg | Actual Value: mountain | Predicted Value: mountain
Image: 2702.jpg | Actual Value: mountain | Predicted Value: mountain
Image: 2615.jpg | Actual Value: mountain | Predicted Value: mountain
Image: 3057.jpg | Actual Value: forest | Predicted Value: forest
Image: 329.jpg | Actual Value: glacier | Predicted Value: mountain
Image: 2468.jpg | Actual Value: forest | Predicted Value: forest

End of Image-clasification..

```

Nakon inicijalizacije modela on se može i koristiti. Pored klasifikacije postoji i model za detekciju objekata ali je već gotovo treniran i ne treba prolaziti kroz fazu treniranja. Izborom dugmića `Browse` se selektuje slike sa filesystem-a, zatim se može klasifikovati ili predati modulu za detekciju objekata klikom na `Classify` ili `Object detection` dugmiće.

**Pritom, obe procedure beleže rezultate u konzoli a detekcija objekata dodatno isrtava pravougaonike sa klasnim labelama oko detektovanih objekata.**

![alt text][screenshot_02]

[screenshot_02]: screenshots/screenshot_02.png

Ovo je bio primer uspešne klasifikacije učitane slike - ovo je slika van dataseta koju model prvi put sada prepoznaje. Može se videti rezultat klasifikacije kao scene **sea**.

![alt text][screenshot_01]

[screenshot_01]: screenshots/screenshot_01.png

Još jedan primer klasifikacije slike - u ovom slučaju druge klase.

![alt text][screenshot_03]

[screenshot_03]: screenshots/screenshot_03.png

Ovo je primer detekcije objekata, detektovani objekti se beleže u konzoli - a rezultat sa vrednostima sigurnosti se može videti u konzoli.

![alt text][screenshot_04]

[screenshot_04]: screenshots/screenshot_04.png

Još jedan malo očigledniji primer detekcije objekata.
