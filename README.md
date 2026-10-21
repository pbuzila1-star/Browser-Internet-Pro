# InternetPro

Browser Android minimal construit cu .NET MAUI și WebView.

## Funcții

- Navigare web, căutare, acasă, înainte și înapoi.
- APK Android cu numele aplicației InternetPro și iconiță dedicată.
- Interfață comună .NET MAUI pentru Android și iPhone; codul UI este C#/XAML, nu Swift.
- Istoric local limitat la ultimele 100 de adrese și acțiune de ștergere.
- Descărcări prin serviciul Android DownloadManager, în folderul `Downloads/Browser` de pe dispozitiv.
- Ștergerea cache-ului WebView, cookie-urilor, datelor Web Storage și istoricului când fereastra aplicației intră în fundal.
- Versiunea aplicației este afișată în interfață.
- Mod dezvoltator opțional pentru WebView.
- Safe Browsing Android, ferestre multiple dezactivate și o listă locală de domenii/termeni pentru filtrarea navigării.

## Compatibilitate

- Compatibilitate minimă configurată: Android 10 (API 29) sau mai nou și Android System WebView disponibil. Android 1–9 nu sunt suportate; nu este impus un plafon la Android 17.
- Compatibilitatea efectivă depinde și de versiunea WebView și de memoria dispozitivului; nu toate modelele au fost testate fizic.
- Telefoanele Oppo, Samsung și Xiaomi sunt compatibile dacă rulează Android 10/API 29 sau mai nou și au Android System WebView. Marca singură nu garantează compatibilitatea.
- Alte mărci, inclusiv E-Star, Emochi sau modele mai puțin cunoscute, trebuie verificate după versiunea Android, disponibilitatea WebView și memoria dispozitivului; compatibilitatea lor nu este confirmată automat.
- Android TV/Google TV poate fi compatibil dacă rulează Android 10/API 29 sau mai nou și are Android System WebView, dar interfața cu telecomanda și publicarea pentru TV nu sunt încă testate sau optimizate.
- APK-ul Android nu se poate instala pe iPhone/iPad; proiectul are un target iOS separat, minim iOS 15, care trebuie publicat ca `.ipa`.
- Incompatibile: PC-uri eMachines cu Windows, Pear OS, televizoare Samsung cu Tizen și alte sisteme non-Android. Televizoarele analogice/electromecanice din anii 1950 nu pot instala aplicații Android sau iOS.
- Pentru Samsung TV este necesară o versiune separată pentru Tizen și publicarea/certificarea ei în magazinul Samsung; APK-ul Android nu poate fi inclus direct în acel magazin.

## Limitări importante

Filtrul este o protecție de bază pentru navigarea către domenii și adrese cunoscute. Nu filtrează toate cererile de reclame din interiorul paginilor, nu garantează detectarea malware-ului și nu înlocuiește un antivirus. Blocarea conținutului integrat în pagini și sugestiile unui site necesită un filtru de resurse actualizat sau logică specifică site-ului.

Aplicația afișează versiunea curentă, însă actualizările automate sunt gestionate de magazinul din care este instalată. Nu este configurată o distribuție în Google Play sau un serviciu propriu de actualizare.

Ștergerea datelor la trecerea în fundal deloghează utilizatorul de pe site-uri. Android poate opri forțat procesul fără să livreze evenimentul de fundal, deci curățarea la oprirea forțată nu poate fi garantată.

Aplicația nu poate selecta benzile Wi-Fi sau generații de rețea celulară; acestea sunt controlate de sistem și hardware.

## Build Android

Necesită .NET 10 SDK, workload-ul `maui-android`, Android SDK și un emulator sau dispozitiv Android pentru rulare. După build, deschiderea fișierului APK pe telefon afișează instalatorul Android și cere confirmarea utilizatorului.

```powershell
dotnet workload install maui-android
dotnet publish BrowserPro.csproj -c Release -f net10.0-android
```

Android SDK este instalat în profilul utilizatorului, iar publicarea Release a reușit. APK-ul semnat este disponibil direct în `InternetPro.apk`; artefactele complete de build sunt și în `bin/Release/net10.0-android`.

Setările pachetului sunt în `InternetPro.config`. Build-ul Release folosește linking pentru a reduce dimensiunea APK-ului; APK-ul este deja comprimat și poate fi instalat direct pe Android. Publicarea pentru distribuție necesită și semnarea cu un certificat de release.

## Build iOS

Targetul iOS se activează pe macOS. Necesită un Mac cu Xcode și workload-ul .NET MAUI iOS; publicarea pentru iPhone produce un `.ipa` și necesită semnare Apple. Descărcările și ștergerea datelor WebView sunt momentan implementate doar pentru Android și trebuie completate/testate separat pe iOS.