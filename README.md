# ML.NET

**ML.NET** je kros-platformski *framework* za mašinsko učenje namenjen da bude korišćen u **.NET** okruženju. Neke od osnovnih mogućnosti su integracija modela mašinskog učenja u postojeće .NET sisteme, kros-platformski CLI i slično.

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
