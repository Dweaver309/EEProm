Imports System
Imports Microsoft.SPOT
Imports Microsoft.SPOT.Hardware
Imports System.Threading
Imports Microsoft.VisualBasic.Strings
Public Class EEprom24LC256
    'Private Shared I2CConfig As I2CDevice.Configuration = New I2CDevice.Configuration(80, 400)

    Private Shared I2C As I2CDevice
    ''' <summary>
    ''' Initializes a new instance of the <see cref="PandaEEprom"/> class.
    ''' </summary>
    ' Public Sub New()

    'I2CConfig = New I2CDevice.Configuration(CUShort(Address >> 1), ClockRateKHz)
    ' 400 KHz, A0-A2 shorted to ground (by design) = 0x50
    'See this adddress would be 0x54
    'https://learn.sparkfun.com/tutorials/reading-and-writing-serial-eeproms?_ga=2.108951682.187827993.1506340160-1561098805.1424355686&_gac=1.87695722.1505836330.CjwKEAjwgIPOBRDn2eXxsN7S4RcSJABwNV90yvMlGAL86ZfVRkJsPS4LAteI0OhP0edbJN72UPjydBoCEpHw_wcB
    '0x54 is 84 decimal
    'I2CConfig = New I2CDevice.Configuration(84, 400)
    Public Sub New(Address As Byte, ClockRateKHz As Integer)
        Dim I2CConfig = New I2CDevice.Configuration(CUShort(Address), ClockRateKHz)
        Dim i As UShort = CUShort(Address)
        Debug.Print("Address " & i.ToString)
        ' 400 KHz, A0-A2 shorted to ground (by design) = 0x50
        I2C = New I2CDevice(I2CConfig)
    End Sub



    ' I2C = New I2CDevice(I2CConfig)
    'End Sub

    ''' <summary>
    ''' Writes a one byte data at the specified address.
    ''' </summary>
    ''' <param name="Address">The address to write to.</param>
    ''' <param name="data">The byte to write</param>
    'Public Sub Write(Address As Integer, data As Byte)
    'Dim xActions = New I2CDevice.I2CTransaction(0) {}
    'so 5 >> 3 = 5 / 2 ^ 3  
    ' Address = CInt(Address / 2 ^ 8)

    'xActions(0) = I2CDevice.CreateWriteTransaction(New Byte() {CByte(Address >> 8), CByte(Address And &HFF), data})
    'xActions(0) = I2CDevice.CreateWriteTransaction(New Byte() {CByte(Address), CByte(Address And &HFF), data})
    'I2C.Execute(xActions, 1000)

    'Thread.Sleep(5)
    ' Mandatory after each Write transaction !!!
    ' End Sub

    ''' <summary>
    ''' Writes the string Text at the specified address.
    ''' </summary>
    ''' <param name="Address">The starting address to write to</param>
    ''' <param name="Text">The text to write to EEprom</param>
    Public Shared Sub Write(Address As Integer, Text As String)
        'Dim Addr = Address
        Dim xActions = New I2CDevice.I2CTransaction(0) {}
        'To save string length "00000"
        Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes("00" & Text)
        ' the "00" string reserves the room for the 2 bytes address that follows

        'buffer(0) = CByte(Address >> 8)

        'so 5 >> 3 = 5 / 2 ^ 3  
        ' Address = CInt(Address / 2 ^ 8)
        buffer(0) = HighByte(Address)

        buffer(1) = LowByte(Address)
        xActions(0) = I2CDevice.CreateWriteTransaction(buffer)
        Thread.Sleep(5)
        I2C.Execute(xActions, 1000)
        Thread.Sleep(5)
        If I2C.Execute(xActions, 1000) = 0 Then
            Debug.Print("Failed to perform I2C transaction")
        Else
            Debug.Print("Register value: " + buffer(0).ToString())
        End If

        ' Mandatory after each Write transaction !!!
    End Sub

    ''' <summary>
    ''' Reads the specified address.
    ''' </summary>
    ''' <param name="Address">The address to be read</param>
    ''' <returns>One byte from the EEprom</returns>
    Public Shared Function Read(Address As Integer) As String
        Dim Data = New Byte(0) {}
        Dim xActions = New I2CDevice.I2CTransaction(0) {}
        'so 5 >> 3 = 5 / 2 ^ 3  
        'Dim HighByte1 As Byte = CByte(Address / 2 ^ 8 & &HFF)
        'Dim HighByte1 As Byte = CByte(Address)

        'Debug.Print("Address High Byte " & HighByte1.ToString)
        'Dim LowByte1 As Byte = CByte(Address And &HFF)
        'Debug.Print("Address Low Byte " & LowByte1.ToString)

        'Dim hb As Byte = HighByte(Address)
        'Debug.Print("High Byte From Function " & hb.ToString)
        'Dim lb As Byte = LowByte(LowByte(Address))
        'Debug.Print("Low Byte From Function " & lb.ToString)
        xActions(0) = I2CDevice.CreateWriteTransaction(New Byte() {HighByte(Address), LowByte(Address)})
        Thread.Sleep(5)
        ' Mandatory after each Write transaction !!!
        I2C.Execute(xActions, 1000)
        'Thread.Sleep(5)
        xActions(0) = I2CDevice.CreateReadTransaction(Data)
        I2C.Execute(xActions, 1000)
        'Thread.Sleep(5)
        'Debug.Print(ChrW(t).ToString)

        Return ChrW(Data(0)).ToString
    End Function
    Private Shared Function HighByte(ReadAddress As Integer) As Byte
        Dim AddHigh As Byte = CByte(ReadAddress / 2 ^ 8 & &HFF)
        Return AddHigh
    End Function
    Private Shared Function LowByte(ReadAddress As Integer) As Byte
        Dim AddrLow As Byte = CByte(ReadAddress And &HFF)
        Return AddrLow
    End Function
End Class
