Jag har valt ut tre delar av applikationen som jag tänker mig kan gå fel och gjort tester för dessa.

1. CreateInitialUsers();
   Jag skrev ett test för att kolla dessa så de verkligen skapades, samt att de får sina konton skapade till sig.
   Skulle denna funktionen inte funka skulle det inte finnas några konton eller användare att logga in med.

3. CurrencyConverter
   Jag skrev testar för att kontrollera så currency convertern konverterade pengar korrekt mellan olika valutor,
   och även att den la ihop totalsumman för alla pengar korrekt, samt så kontrollerar jag att den skickar ett exception om en valuta inte känns igen.
   Skulle konverteraren konvertera fel skulle det innebära stora skillnader i värdet på pengarna och banken skulle kunna förlora stora summor pengar.

4. HandleTransactions();
   Jag skrev två tester som kontrollerar vad som händer vid en transaction. Ett test kollar så att det inte går att föra över pengar om man inte har den summan på sitt konto,
   och den andra kontrollerar så att en överföring faktikt föröver pengar genom att kolla så rätt summan dragits och lagts till på rätt konto, samt att transactionen har loggats korrekt.
