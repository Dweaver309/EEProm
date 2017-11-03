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

        EEprom24LC256.Initiate(84, 400)
        Dim t As String = String.Empty

        EEprom24LC256.Write(EEprom24LC256.Address.SSID, "ssid")
        t = EEprom24LC256.Read(EEprom24LC256.Address.SSID)
        Debug.Print("SSID " & t)
       

        EEprom24LC256.Write(EEprom24LC256.Address.Password, "Password")
        t = EEprom24LC256.Read(EEprom24LC256.Address.Password)
        Debug.Print("Password " & t)

        If EEprom24LC256.Exist(EEprom24LC256.Address.Password) Then
            Debug.Print("Password Exists")
        Else
            Debug.Print("Password Does not Exist")
        End If

    End Sub

End Module
