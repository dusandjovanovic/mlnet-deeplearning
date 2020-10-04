# ML.NET

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

![alt text](https://docs.microsoft.com/en-us/dotnet/machine-learning/media/model-builder-steps.png "Model builder")

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

![alt text](https://docs.microsoft.com/en-us/dotnet/machine-learning/media/automate-training-with-cli/mlnet-classification-powershell.gif "CLI")

Izlaz koji se dobija sadrži više stavki:

* Serijalizovani `.zip` modela koji može da se koristi
* C# kod:
** C# kod za pokretanje/ocenjivanje modela u .NET okruženju
** C# kod sa kodom koji se koristio za treniranje modela
* Log fajl koji sadrži korisne informacije u procesu izgradnje modela.

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
* Pripremanje ulaznih podataka za mašinsko učenje zahteva ove transformacije.
** Svaka transformacija očekuje i proizvodi podatke u odredjenom obliku, koji se navode u ulančanoj specifikaciji:
*** Mapiranje i grupisanje kolona
*** Normalizacija i skaliranje
*** Konverzije izmedju tipova
*** Transformacije teksta/slika poput **izvlačenja piksela**
*** **Transformacije deep learning modela** poput `LoadTensorFlowModel` i druge..
