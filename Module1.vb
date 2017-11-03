Imports Microsoft.SPOT
Imports Microsoft.SPOT.Hardware
Imports SecretLabs.NETMF.Hardware
Imports SecretLabs.NETMF.Hardware.Netduino
Imports System
Imports System.Net
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Constants
Imports System.Text
Module Module1

    Sub Main()
        ' write your code here
        Dim EE As New EEprom24LC256(84, 400)
        EEprom24LC256.Write(1024, "a")
        ' Thread.Sleep(1000)

        EEprom24LC256.Write(1025, "b")
        'Thread.Sleep(1000)
        EEprom24LC256.Write(1026, "c")
        'Thread.Sleep(1000)
        ' For i = 1024 To 1027

        For i = 1024 To 1026
            ' Dim t As Byte = EEprom24LC256.Read(i)
            'Debug.Print(AscW(CChar(t.ToString))
            'Debug.Print(ChrW(t).ToString)
            Debug.Print(EEprom24LC256.Read(i))
            'Thread.Sleep(1000)
        Next
        ' Next
    End Sub

End Module
