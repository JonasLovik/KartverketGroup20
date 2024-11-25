# KartverketGroup20

Her er gruppe 20 sin prototype til Kartverket. Denne README- filen vil fungere som en guide til å bruke web applikasjonen og en dokumentasjon for hvordan man kan sette opp, forstå og jobbe med koden i repositorien.

## Samarbeidspartnere
- https://github.com/JonasLovik
- https://github.com/HenrikLor1tzen
- https://github.com/sigurdmaeland
- https://github.com/sigurdbrekke
- https://github.com/Zakahashi03
- https://github.com/oleols1

 ## Oversikt
Denne applikasjonen er basert på ett prosjekt som er gitt av Kartverket, der målet er å lage en løsning slik at brukere kan melde inn feil i ett kart, og en saksbehandler kan enkelt håndtere disse innsendingene. Denne applikasjonen gir en enkel og brukervennlig måte for brukere å sende innmeldinger og det er enkelt for saksbehandlere å håndtere innmeldingene.

## Eksempel på bruk
### For bruker:
#### 1.	Gå inn på nettsiden
#### 2.	Bruk menyen for å navigere til logg inn eller registrer bruker
---
![image](https://github.com/user-attachments/assets/1e8d5229-8d00-4f1a-a39e-bcb9bb273324)
![image](https://github.com/user-attachments/assets/60d5bbb4-c662-44c2-bd0f-605ee589c08b)
![image](https://github.com/user-attachments/assets/ca9f921c-2aff-4741-ab03-2bccf8dcaaad)
---

#### 3.	Naviger inn på enten «Til lands» eller «Til sjøs» avhengig av hvilken type kart man vil melde feil i
#### 4.	Velg der man har oppdaget en feil og skriv en beskrivelse
---
![image](https://github.com/user-attachments/assets/bf884576-439a-4caf-b2fb-4a57d9994033)
---
#### 5.	Man kan så navigere seg inn på «Mine Rapporter» ved hjelp av menyen og se sine egne innsendinger og status på disse.
---
![image](https://github.com/user-attachments/assets/117d5a71-e54b-47d0-9bbe-d538c4bb9d48)
---
### For saksbehandler:
#### 1.	Logg inn som en administrator
#### 2.	Åpne menyen og her vil man få opp ett valg «For ansatte»
#### 3.	Her vil saksbehandler få opp alle innsendte rapporter
---
![image](https://github.com/user-attachments/assets/48da6177-5a0a-4b56-bda1-80625e9f443f)
---

#### 4.	Saksbehandler kan da velge om rapporten skal godkjennes eller ikke.
---
![image](https://github.com/user-attachments/assets/2c225745-36ec-4514-8e8d-d4cb914c42ad)
---

<br><br>
## Oppsett av repository

Klon repositoriet:

```bash
https://github.com/JonasLovik/KartverketGroup20.git
```

Naviger til prosjektmappen:

```bash
cd KartverketGroup20
```
Programmet er laget med .NET og kjøres med Docker Compose. Ved hjelp av en IDE som Visual Studio kan programmet bygges.
Sørg for at databasetilkobling og andre instillinger er konfigurert slik at det passer ditt bruk.

## Struktur
Dette prosjektet følger en moderne lagdelt MVC struktur. Dette er på grunn av at den kombinerer MVC med tjenester og API-støtte. 

![image](https://github.com/user-attachments/assets/0d09ef4b-c1fb-47e5-b4e3-12e98ab1b29e)

## Komponenter
| **Komponent**         | **Beskrivelse**                                                                                     |
|------------------------|-----------------------------------------------------------------------------------------------------|
| **KartverketGroup20** | Hovedprosjektet som inneholder applikasjonen.                                                      |
| **APIModels**      | Inneholder API modell data for Kartverket sitt API            |
| **Controllers**       | Inneholder alle controllere som styrer backend av applikasjonen                              |
| **Data**          | Inneholder database context og enum som er brukt                          |
| **Migrations**          | Inneholder SQL statements for å opprette database og ulike tabeller         |
| **Models**               | Inneholder objekter og restriksjoner for databaseobjekter                                          |
| **Services**     | Inneholder metoder for Kartverkets API og SQL statements for å hente data fra rapport tabellen i databasen                                      |
| **ViewModels**              | Inneholder view-modeler som vises data i nettsiden                           |
| **Views**            | Inneholder styling og det som faktisk blir vist på nettsiden  |

## Teknologi
- **ASP.NET Core MVC:** Brukt på grunn av sin balanse mellom fleksibilitet, ytelse og skalerbarhet, og innebygd støtte for Model-View-Controller(MVC)-arkitektur der controller (C) styrer url logikken og henter objekter fra modellen (M) og sender det videre til ett view (V) som vises på skjemen
- **C#:** Programmerings språk brukt for backend logikk
-  **MySQL/MariaDB:** Brukt som databasesystem på grunn av skalerbarhet og støtte
- **Kartverket API:** For å finne informasjon om ulike kommuner og steder i Norge 
- **Leaflet.js:** Brukt for å vise nøyaktig kartlegging for visualisering av geodata
- **Entity Framework & Identity:** Brukt for å håndtere autorisasjon og autentisering på grunn av integrasjonen mellom Entity Framework & identity
- **Dapper:** Gir enkel og effektiv databaseintegrasjon med MySQL/MariaDB og en fleksibel tilnærming til spørringer


### NB: til Rania og Espen
Gruppen hadde problemer med git og docker, vi måtte bytte repo og hentet kode fra tidligere repo. Dette er grunnen at ikke hele gruppen vises som collaborators og få commits. 

## Lisens
Lisensen brukt for prosjektet er Apache-2.0 license
<br><br>
Les mer om lisenset her: https://choosealicense.com/licenses/apache-2.0/


