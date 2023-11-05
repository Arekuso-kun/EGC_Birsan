# EGC_Birsan

Bîrsan Dorin-Alexandru

Grupa 3132a

## Intrebari Laborator 2

1. Ce este un viewport?

  Un viewport este o zona ferestrei in care vor fi desenate obiecte. Acesta are ca parametri x, y, latimea si inaltimea.

2. Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?

  FPS reprezinta numarul de cadre afisate pe secunda in cadrul unei aplicatii.

3. Când este rulată metoda OnUpdateFrame()?

  Metoda OnUpdateFrame() este rulata la fiecare cadru, înainte de a il desena, dar depinde si de parametrii functiei Run().

4. Ce este modul imediat de randare?

  Modul imediat este o abordare veche de randare.

5. Care este ultima versiune de OpenGL care acceptă modul imediat?

  OpenGL 3.2 este ultima versiune care accepta modul imediat.

6. Când este rulată metoda OnRenderFrame()?

  Metoda OnRenderFrame() este rulata la fiecare cadru dupa OnUpdateFrame().

7. De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?

  OnResize() initializeaza parametrii de vizualizare pentru a evita distorsiuni sau probleme de scalare.

8. Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?

  Parametrii includ unghiul de vedere (radiani), raportul de aspect, distanta la planul apropiat si distanta la planul indepartat, toti parametri fiind float.


## Intrebari Laborator 3

1. Care este ordinea de desenare a vertexurilor pentru aceste metode (orar sau anti-orar)?

   Ordinea de desenare a vertexurilor poate fi definita fie in sens orar fie in sens anti-orar, in functie de modul in care doriti sa defineasca orientarea triunghiurilor.
  
2. Ce este anti-aliasing? Prezentați această tehnică pe scurt.

   Anti-aliasing este o tehnica folosita in grafica pentru a imbunatati calitatea imaginilor digitale prin estomparea marginilor aspre si eliminarea efectului de zimtare.
  
3. Care este efectul rulării comenzii GL.LineWidth(float)? Dar pentru GL.PointSize(float)? Funcționează în interiorul unei zone GL.Begin()?

  GL.LineWidth(float) stabilește grosimea liniilor desenate în OpenGL pentru primitivele de linie, cum ar fi GL_LINES. GL.PointSize(float) setează dimensiunea punctelor desenate pentru primitivele de punct, cum ar fi GL_POINTS. Acestea nu funcționează în interiorul unui bloc GL.Begin().
  Comanda GL.LineWidth(float) stabileste grosimea liniilor, si comanda GL.PointSize(float) stabileste dimensiunea punctelor. Aceste comenzi trebuie folosite in afara sectiunii GL.Begin() - GL.End() si nu au efect in interiorul sectiunii.

4. Răspundeți la următoarele întrebări (utilizați ca referință eventual și tutorii OpenGL Nate Robbins):
  - Care este efectul utilizării directivei LineLoop atunci când desenate segmente de dreaptă multiple în OpenGL?
    
    LineLoop creeaza un contur inchis prin conectarea punctelor pentru a forma o bucla inchisa.
    
  - Care este efectul utilizării directivei LineStrip atunci când desenate segmente de dreaptă multiple în OpenGL?
    
    LineStrip deseneaza linii consecutive intre punctele specificate, fara a crea o bucla inchisa.
    
  - Care este efectul utilizării directivei TriangleFan atunci când desenate segmente de dreaptă multiple în OpenGL?
    
    TriangleFan foloseste primul punct ca varf de baza si creeaza triunghiuri radiale in jurul acestuia.
    
  - Care este efectul utilizării directivei TriangleStrip atunci când desenate segmente de dreaptă multiple în OpenGL?
    
    TriangleStrip deseneaza triunghiuri conectate in ordine consecutiva, creand o banda continua de triunghiuri.

6. De ce este importantă utilizarea de culori diferite (în gradient sau culori selectate per suprafață) în desenarea obiectelor 3D? Care este avantajul?
   
  Utilizarea culorilor diferite in desenarea obiectelor 3D este importanta pentru:

  - Perceptia adancimii: Culorile ajuta la evidentierea reliefului si a adancimii.

  - Diferentiere: Ajuta la distingerea clara intre obiecte si parti ale acestora.

  - Estetica: Imbunatateste aspectul vizual al scenei.

  - Comunicare: Culorile transmit informatii suplimentare despre obiecte.

  - Identificarea problemelor: Ajuta la identificarea erorilor si problemelor in modelele 3D.
  
7. Ce reprezintă un gradient de culoare? Cum se obține acesta în OpenGL?

  Un gradient de culoare reprezinta o tranzitie treptata intre doua sau mai multe culori. Se poate obtine un gradient folosind shader-urile pentru a interpola culorile in functie de pozitia sau unghiul de vizualizare a fragmentelor pe ecran.

10. Ce efect are utilizarea unei culori diferite pentru fiecare vertex atunci când desenați o linie sau un triunghi în modul strip?

  Atunci cand se utilizeaza culori diferite pentru fiecare varf al unei linii sau a unui triunghi desenate in modul strip, OpenGL va interpola culorile intre varfuri, creand un efect de gradient sau o tranzitie lina de culoare pe linie sau pe intreaga suprafata a triunghiului.
 
