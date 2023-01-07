POLITECHNIKA BIAŁOSTOCKA
WYDZIAŁ INFORMATYKI

PRACA DYPLOMOWA INŻYNIERSKA

TEMAT: WYKORZYSTANIE PLATFORMY BLAZOR NA
PRZYKŁADZIE APLIKACJI UMOŻLIWIAJĄCEJ TANCERZOM
FORMOWANIE SPOŁECZNOŚCI

WYKONAWCA: DAMIAN KRAWCZUN

OPIEKUN PRACY DYPLOMOWEJ : DR HAB. INŻ. IRENEUSZ JAN MROZEK

BIAŁYSTOK 2022 ROK

Subject of diploma thesis
USING BLAZOR PLATFOR ON AN EXAMPLE APPLICATION THAT
LETS DANCERS FORM COMMUNITIES

Warstwy frontendowe i backendowe zrealizowane są przy użyciu Blazor'a. Baza danych zaimplementowana jest w MS SQL Server.

INSTRUKCJA URUCHOMIENIA:
W przypadku braku wymaganych paczek NuGet należy zgodzić się w dowolnym momencie procesu uruchamiana na doinstalowanie ich.
Do poprawnego uruchomenia aplikacji poleca się użycie środowiska Visual Studio (2019 lub nowsze). 
Z pomocą tego środowiska należy uruchomić plik projektu (rozszerzenie .sln) znajdujący się w głównym folderze.
Zaleca się zaimportowanie struktury bazy danych wraz z danymi. W tym celu należy użyć wbudowanego w Visual Studio SQL Server Object Explorera.
Do lokalnego serwera SQL należy zaimportować bazę z pliku DancePlatform.dacpac znajdującego się w folderze dBase (./'katalog główny projektu'/dbase).
W celu uruchomienia aplikacji należy w eksploratorze rozwiażań odnaleźć projekt Server (katalog: Rozwiązanie "DancePlatform" -> Source -> Web -> Server).
Projekt ten powinien zostać ustawiony jako projekt startowy (prawy przycisk myszy -> Ustaw jako projekt startowy).
Klikając na klawiaturze 'F10' lub na przycisk uruchomienia projektu na interfejsie powinniśmy móc uruchomić projekt.
Otworzy się strona internetowa na widoku logowania. Na dole formularza umieszczone są przyciski uzupełniające dane logowania dla odpowiednich kont.
Poleca się skorzystanie z nich celem testowania aplikacji.
W przypadku braku otwartej strony internetowej po uruchomieniu projektu należy użyć adresu:
https://localhost:44398
lub https://localhost:5001