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

## ML.NET osnovna arhitektura aplikacija

Aplikativna struktura se svodi na iterativni proces razvoja modela koji se sastoji iz više koraka:

* Skupljanje i učitavanje podataka `IDataView` objekat
* Specifikacija `pipleine-a` operacija ekstrakcije podatka (feature atributa) i aplikacije algoritama mašinskog učenja
* Treniranje modela pozivom metode `Fit()` nad pipeline-om
* Evaluacije modela i poboljšanja kroz više iteracija
* Čuvanje modela u binarnom formatu
* Učitavanje modela nazad u `ITransformer` objekat
* Formiranje predikcija pozivom `CreatePredictionEngine.Predict()`

```charp
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

           // label Attribute promenljiva sadrži predikciju na osnovu feature atributa
       }
```

U ovom pokaznom kodu može se videti najosnovniji primer učitavnja podataka, definisanja pipeline-a, treniranja i naravno korišćenja predikcija.
