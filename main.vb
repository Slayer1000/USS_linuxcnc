Module VBModule
    Sub Main()
    'takee the Class for USS
    DIM USS As NEW USSCLASS

    '########################################################################################
    ' next step Port Connection with variabel
        Console.WriteLine("USS_Connection")
        Console.WriteLine("Set your Connection Settings")
        Console.WriteLine("baud rates? (Example: 9600, 14400, 19200")
        Dim rate as String = console.Readline()
        Console.WriteLine("serial port? (Example: dev/ttyS0")
        Dim port as String = console.Readline()
        Console.WriteLine("VFD Adress ? (Example: 0, 1, 2, ...")
        Dim VFD_Adress as String = console.Readline()
        Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate, VFD_Adress)  
    ' ########################################################################################

  ' Open Port with port and baud rates from the Class
    USS.PortOpen()

Dim funktion_USS As String
        Console.WriteLine("Select your funktion")
        Console.WriteLine("1: RunMMS")
        Console.WriteLine("2: StopRunning")
        Console.WriteLine("3: ReverseRun")
        Console.WriteLine("4: RunMMSOnJOGMode")
        Console.WriteLine("5: ReverseJOG")
        Console.WriteLine("6: StopJOG")
        Console.WriteLine("7: ReqParam")
        Console.WriteLine("8: Exit")
        Console.WriteLine()
        
        
do
        Console.WriteLine("Select the USS funktion?")
        Console.WriteLine()
        funktion_USS = Console.ReadLine()

  select case funktion_USS
    Case 1
        Console.WriteLine("You choose RunMMS")
        Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate, VFD_Adress)
        Console.WriteLine()
        Console.WriteLine("enter the frequency")
        Dim VFD_Freq as String = console.Readline()
        Console.WriteLine("VFD with start with {0} Hz", VFD_Freq)

        USS.RunMMS(VFD_Freq, VFD_Adress)

    Case 2
        Console.WriteLine("You choose StopRunning")
        Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate, VFD_Adress)
        USS.StopRunning(VFD_Adress)

    Case 3
        Console.WriteLine("You choose ReverseRun")
        Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate, VFD_Adress) 
        Console.WriteLine()
        Console.WriteLine("enter the frequency")
        Dim VFD_Freq as String = console.Readline()
        Console.WriteLine("VFD with start reverse with {0} Hz", VFD_Freq)

        USS.ReverseRun(VFD_Freq, VFD_Adress)


    Case 4
        Console.WriteLine("You choose RunMMSOnJOGMode")
        Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate, VFD_Adress) 
    	USS.RunMMSOnJOGMode(VFD_Adress)

    Case 5
        Console.WriteLine("You choose ReverseJOG")
        Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate, VFD_Adress) 
        USS.ReverseJOG(VFD_Adress)

    Case 6
        Console.WriteLine("You choose StopJOG")
        Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate, VFD_Adress)
        USS.StopJOG(VFD_Adress)

    Case 7
        Console.WriteLine("You choose ReqParam")
        Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate, VFD_Adress) 
        USS.ReqParam(VFD_Adress)

    Case Else
        Console.WriteLine("Exit")
      Exit Do
  end select
  
loop

  End Sub
End Module

