
Boolean op= true;                //Esto es para salir del while principal.
 int[] retiros= new int[10];     //Aqui se almacenan los retiros de los usuarios. 
int billetes=0, monedas=0;       //Esto es para sumar la cantidad de billetes y monedas que se usaron.
int numRetiros=0;                //Esto es para saber cuantos retiros hubieron.
Boolean caso2= false;            //Esta variable es para que n o sea nesesario reiniciar el programa si no que se pueda hacer mas retiros.





while(op){   //Este es el while principal en donde esta el menu 

        Console.WriteLine("--------------- Banco CEDIS------------------");
        Console.WriteLine("1.Ingresar la cantidad de retiros echos por usuarios \n2.Revisar la cantidad de billetes y monedas.");
        var respuesta =int.Parse(Console.ReadLine());
        opciones(respuesta); 
        op= resp(caso2);
        caso2=false;
}




void opciones(int respuesta){  //Aqui es donde la opcion que hallan ingresado se ramifica y se resuelve.

    switch (respuesta){
        case 1:   //Aqui se llenan el array de retiros con la validacion que sea menor o igua a 10 ya que solamente se puede asi.
            Console.WriteLine("Ingrese cuantos retiros hubieron (Maximo 10)");
            numRetiros= int.Parse(Console.ReadLine()); 
            if(numRetiros <= 10){
                llenarArray(retiros, numRetiros);
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }else{
                Console.Write("Solo se pueden 10 episodios por favor acatar la instruciones");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                break;
            }
            break;

        case 2: //Aqui se llama a la funcion que desglosa los billetes para saber cuantos son de cada quien. 

            caso2=true; //esto es para saber si ya entro para despues borar el array para que lo pueda hacer enecimas veces.
            BilletesMonedas(retiros, numRetiros);
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            break;

        default:
            Console.WriteLine("No existe ninguna de esas opciones");
            break;

    }
}

void llenarArray(int[] retiros, int numRetiros)  //Como su nombre lo dice llena el Array, tambien validando que este entre el rango de 0 y 50000.
{

    for(int i=0; i< numRetiros; i++){
        Console.Write($"Escrive el monto del {i+1} retiro:");
        retiros[i]= int.Parse(Console.ReadLine());
        
        if(retiros[i] <0 ||  retiros[i] > 50000){
            Console.Write("Solo se pueden ingresar datos mayores de 0 y menores de 50000\n");
            retiros[i]=0;
            break;
        }
    }

}

void BilletesMonedas(int[] retiros, int veces){ //Aqui se desglosa las Monedas y Billetes para despues imprimirlo.

   int[] BilletesMonedas= new int[8];

        for(int i=0; i< veces; i++){
             
        if (retiros[i] >= 500)
        {
            billetes += retiros[i] / 500;
            BilletesMonedas[0]= retiros[i]/500;
            retiros[i] %= 500;
        }
        
            
            if(retiros[i] >= 200){
                billetes+= retiros[i]/200;
                BilletesMonedas[1]= retiros[i]/200;
                retiros[i] %=200;
             }
                
            if(retiros[i] >= 100){
                billetes+= retiros[i]/100;
                BilletesMonedas[2]= retiros[i]/100;
                retiros[i] %= 100;
            }

            if(retiros[i] >= 50){
                
                billetes+= retiros[i]/50;
                BilletesMonedas[3]= retiros[i]/50;
                retiros[i] %= 50;
            }

            if(retiros[i] >= 20){
                billetes+= retiros[i]/20;
                BilletesMonedas[4]= retiros[i]/20;
                retiros[i] %=20;
            }

            if(retiros[i] >= 10){
                monedas+= retiros[i]/10;
                BilletesMonedas[5]= retiros[i]/10;
                retiros[i] %= 10;
            }

            if(retiros[i] >= 5){
                monedas+= retiros[i]/5;
                BilletesMonedas[6]= retiros[i]/5;
                retiros[i] %= 5;
            }

            if(retiros[i] >= 1){
                monedas+= retiros[i]/1;
                BilletesMonedas[7]= retiros[i]/1;
                retiros[i] %= 1;
            }

          Console.Write("\nRetiro #" + (i + 1) + "\n" +
              "Billetes entregados:\n" +
              "  " + BilletesMonedas[0] + " billetes de 500 + " + BilletesMonedas[1] + " billetes de 200 + " + BilletesMonedas[2] + " billetes de 100 +" + BilletesMonedas[3] + " " +"billetes de 50 +" + " " + BilletesMonedas[4] + " " +"billetes de 20 =" + " "+billetes + "\n" +
              "  Monedas entregadas:\n " + " " +BilletesMonedas[5] + " "+"monedas de 10 +"+ " " + BilletesMonedas[6] +" " +"monedas de 5 +" + " " +BilletesMonedas[7] + " "+ "Monedas de 1="+" "+ monedas +"\n");
            billetes=0;
            monedas=0;
        }

}


bool resp( Boolean caso2){  //Aqui solamente se pregunta si quieren que se acabe el ciclo o no.
    Console.WriteLine("Quiere hacer otra funcion? SI=1 NO=0");
    String input = Console.ReadLine();
    if(int.TryParse(input, out int op1) ){
        
        if(caso2){ //Aqui  se valida si ya entro a la opcion 2 para despues borrar todos los datos que hay (o bueno no se borran si no se reinicia).

            for(int i=0; i<retiros.Length; i++){ 
                retiros[i]=0;
            }
        }

        return op1== 1;     
        
    }
   return true;
       
}
