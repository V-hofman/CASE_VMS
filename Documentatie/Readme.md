# READ ME
<sub>or dont ¯\\_(ツ)_/¯</sub>

---
<sub>Let op! Dit document is geschreven zodat het in visual studio code met de "[Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)" extension presenteerbaar is. Ik garandeer niet dat dit er mooi uitziet op andere plekken</sub>
## Systeem Eisen:
De frontend is ontwikkeld met:
- Angular versie 15.2.2
- NPM versie: 8.19.3
- Node versie: 18.13.0
- OS: win32 x64

De backend is ontwikkeld met:
- .Net 7
- MSSQL Server

De packages die nodig zijn voor de front-end kunt u installeren via:
`npm i` of `npm install`

De packages die nodig zijn voor de back-end kunt u installeren via:
`dotnet restore`

Hier ga ik vanuit dat de bovenstaande systeem eisen aan worden voldaan, anders kunnen deze niet worden uitgevoerd.

---

## Het is geinstalleerd wat nu?

- Advies is om eerst de database op te zetten:
WORK IN PROGRESS

- Daarna start u de back-end applicatie op:
`dotnet run` of via F5 binnen visual studio.
<sub>deze is te benaderen via https://localhost:7292<sub>

- Daarna start u de front-end applicatie op:
`ng serve`
<sub>Deze is te benaderen via http://localhost:4200<sub> (let op **GEEN HTTPS**)

Deze port nummers zijn de standaard waardes indien deze bezet zijn, kunnen andere mogelijk worden gebruikt. Wilt u weten welke in gebruik worden genomem kunt u het volgende doen:
`ng serve --open` Dit opent meteen een browser met de juiste angular port in uw browser.
Voor de back-end moet u kijken welke port er in de console wordt gelogd tijdens het opstarten (dit kan overigens ook voor de front-end).
