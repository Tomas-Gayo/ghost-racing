# Ghost Racing

## Index

- [Cómo jugar](https://gitlab.com/Tomas-Gayo/pec1-race-game#c%C3%B3mo-jugar)
- [Demo](https://gitlab.com/Tomas-Gayo/pec1-race-game#demo)
- [Puntos importantes](https://gitlab.com/Tomas-Gayo/pec1-race-game#puntos-importantes)
- [Librerías](https://gitlab.com/Tomas-Gayo/pec1-race-game#librer%C3%ADas)
- [Música y sonido](https://gitlab.com/Tomas-Gayo/pec1-race-game#m%C3%BAsica-y-sonidos)
- [Atribuciones](https://gitlab.com/Tomas-Gayo/pec1-race-game#atribuciones)

## Cómo jugar

Existen dos modos de juego: carrera contrareloj, donde se puede correr contra el coche fantasma de la mejor vuelta y; un modo de visualización de la mejor vuelta. En el primer modo, el coche se puede mover con las teclas **WASD** y en el segundo, aunque no se pueda controlar el coche, con la tecla **espacio** se puede cambiar la vista de la camara.

## Demo

Se puede ver la [demostración del juego en video](https://youtu.be/HSTqnJGyjZk) en Youtube para su versión de PC. 

## Puntos importantes

- **El circuito deberá estar ubicado total o parcialmente en un terreno montañoso, pudiendo también hacer partes urbanas.**

El circuito se ha dispuesto en una zona montañosa, con un lago encima de una pequeña meseta que baja hacia una llanura que será la línea de meta. 

<img src="/images/gameScene1.PNG" alt="Escena de juego llanura" width="500"/>
<img src="/images/gameScene2.PNG" alt="Escena de juego montaña" width="500"/>
<img src="/images/gameScene3.PNG" alt="Escena de juego subida" width="500"/>

- **Salir fuera de la pista deberá hacer que el coche vaya más lento.**

Se ha hecho uso de la funcionalidad raycast para detectar hacía abajo (en un solo punto) si debajo del coche hay carretera o no. En caso negativo el coche irá más lento (topspeed se reduce), también se ha tenido en cuenta la marcha atrás ya que funciona con otra variable (ReverseToque). La siguiente imagen demustra la visualización de este rayo en color rojo.

<img src="/images/raycast.PNG" alt="Rayo saliendo de debajo del coche" width="500"/>

- **La carrera constará de 3 vueltas, y deberéis guardar el fantasma del mejor tiempo (todo el recorrido exacto que haya hecho el vehículo durante esa carrera). El fantasma siempre es el resultado de la mejor vuelta de toda la carrera.**

La carrera consta de tres vueltas y siempre se registran los tiempos de vuelta que se mostrarán en pantalla y se guarda el tiempo total de las tres vueltas para poder ser comparado con el mejor tiempo realizado (el mejor tiempo realizado en el juego).
Los datos se gestionan a partir de un _Scriptable Object_ (GhostLapData) que tiene dos pares de listas, un par para guardar la posición y rotación de la carrera actual y otro par que almacena la posición y rotación de la mejor carrera. A continuación, en caso de superar el mejor tiempo se pide al scriptable object (SO) que copie los datos de las listas de la carrera actual en las listas del mejor tiempo. 
Para realizar este guardado llamamos a la función AddNewData desde el script GhostLapRecord, que se incorpora en el objecto que queremos grabar (en este caso el coche) y hará un muestreo de la posición y la rotación para enviarselo al SO. 


- **En la siguiente carrera, el jugador deberá correr contra ese fantasma tal como se puede ver en este video.**

Si en el SO tenemos guardado datos de un mejor tiempo, entonces, se podrá reproducir el fantasma del coche con la mejor carrera (la mejor carrera de todo el juego, no la de la carrera anterior). Esto se puede hacer gracias al script GhostLapPlay que va incorporado al objeto coche fantasma. Este realiza una interpolación a partir de los datos guardados en la lista de mejor carrera, entonces una vez la lista de muestras se acaba el coche se desactiva. 

**NOTA:** La solución difiere del enunciado porque se ha considerado que hacer un fantasma de la mejor vuelta provoca que la primera vuelta siempre sea nula, porque la salida se empieza con el coche parado y la carrera no sería justa. Por lo tanto, se guarda el fantasma de la mejor carrera (3 vueltas) y la reproducción de esta siempre será la mejor del juego no importa cuando se haya realizado esa carrera. 

- **También se deberá poder ver la repetición de esa carrera, tanto con la cámara normal de juego, como por un conjunto de cámaras que simulen una repetición por televisión al estilo de este video.**

A partir del asset Cinemachine se ha conseguido crear un sistema de camaras que van cambiando de vista según si el coche este en su línea de visión o no. La funcionalidad utilizada es el ClearShot junto con el componente cinemachine collider que se adjunta a las camaras.
Entonces, se añaden varias camaras virtuales y estas detectan si el objecto, con un tag específico, está en el campo de visión, ahora bien, muchas veces estos campos se mezcla con otras camaras, por lo que hay una funcionalidad extra que permite obstruir los campos de visión a partir de objetos con layers específicos.

Destacar que al empezar el modo repetición se instancia un objecto (el coche) que utiliza el mismo script que va adjunto al coche fantasma y de esta manera reproducir la mejor carrera.

Todas las camaras utilizadas en la carrera:

<img src="/images/cameras.PNG" alt="Camaras virtuales de la repetición de carrera" width="500"/>

Clear shot recoge el grupo de camaras dispuestas para la repetición:

<img src="/images/clearshot2.PNG" alt="Cinemachine collider componente" width="500"/>

Componente cinemachine collider que muestra el tag que queremos encontrar y el layer que obstruye su campo de visión (collide against):  

<img src="/images/clearshot.PNG" alt="Clear shot objeto" width="500"/>

- **Para que el jugador pueda ver sus tiempos deberá mostrarse por pantalla el tiempo de vuelta actual, y los de las dos vueltas anteriores.**

Como se puede ver en la siguiente imagen, el juego es capaz de mostrar los tiempos de vuelta y el tiempo actual, además, del record actual. 

<img src="/images/HUD.PNG" alt="HUD del juego" width="500"/>

Todo esto es posible gracias al script TimeManager. Este calcula constantemente (si la carrera esta en curso) el tiempo general de juego, entonces, junto con un trigger que funciona como línea de meta (FinishLineTrigger) podemos establecer todos los tiempos de vuelta cada vez que el coche atraviesa ese punto y tiempo total de las 3 vueltas al completar tres vueltas.
Algo a destacar es que se han dispuesto varios checkpoints en la carrera para asegurarse que el coche realiza todo el circuito, por lo que si los checks no son traspasados por el coche, entonces, no podrá activar la línea de meta para establecer los diferentes tiempos.  

## Escenas

El juego consta de 2 escenas: menu incial y escena de juego. La escena de juego se puede ver en la imagen anterior, y el menu incial en la siguiente. 

<img src="/images/menu.PNG" alt="Menu del juego" width="500"/>

Como comentario adicional, en el menu inicial se escoge entre los dos modos, dependiendo del modo escogido el juego desactivará o activará los objetos correspondientes a cada modo de juego ya que se utiliza la misma escena de juego. 

## Estructura de carpetas

<img src="/images/folders.PNG" alt="Estructura de carpetas"/>

## Librerías

- **3DGameProps**: para que el escenario no quede vacío se ha añadido algunos props para decorar el terreno. 

- **Cinemachine**: este paquete se ha utilizado para configurar todas las camaras de la escena de juego.

- **EasyRoads3D**: la carretera se ha podido construir gracias al complejo sistema de construcción del que dispone este paquete. 

- **RCK 2.3**: navegando entre assets de coches se encontro un cielo que encajaba con la idea de este juego, así pues, el cielo nublado es de este asset. 

- **Standard Assets**: aquí se encuentran varias utilidades como son el coche y toda la decoración del entorno como arboles, hierva o texturas. 

- **Text Mesh Pro**: este asset se ha utilizado para mejorar los estilos de los textos en la UI. Funciona igual que los textos normales pero tiene más opciones de personalización.

# Música y sonidos

Se ha añadido una lista de canciones alineadas con el estilo de juego para que se vayan reproduciendo aleatoriamente cada vez que se entra en una escena. Playlist Manager es quien controla este sistema. 
Por otro lado, se han añadido sonidos de UI para las acciones de hover y click. También, se pueden escuchar dos sonidos en el countdown al principio de carrera para dar feedback al usuario a partir de sonidos de que la carrera esta apunto de empezar. 

## Atribuciones

- **Música**: [Usuario RGA-GT](https://opengameart.org/content/the-best-of-rga-gt-music-pack)

- **Sonidos UI (hover)**: [Usuario NenadSimic](https://freesound.org/people/NenadSimic/sounds/171697/)

- **Sonido UI (click)**: [Usuario newlocknew](https://freesound.org/people/newlocknew/sounds/517379/)

- **Cuenta atrás**: [Usuario StumpyStrust](https://opengameart.org/content/ui-sounds)







