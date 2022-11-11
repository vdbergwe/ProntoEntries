Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
<MetadataType(GetType(Meta_Entry))>
Partial Public Class Entry

End Class

Partial Public Class Meta_Entry
    Public Property EntryID As Integer
    <DisplayName("Participant")>
    Public Property ParticipantID As Nullable(Of Integer)
    <DisplayName("Event Name")>
    Public Property RaceID As Nullable(Of Integer)
    <DisplayName("Division")>
    Public Property DivisionID As Nullable(Of Integer)
    Public Property Amount As Nullable(Of Double)
    Public Property Status As String
    Public Property PaymentReference As Nullable(Of Integer)
    Public Property PayFastReference As String
    Public Property PayFastStatus As String
    Public Property DistanceChange As Nullable(Of Boolean)
    Public Property ChangePaymentRef As String
    Public Property TransferID As Nullable(Of Integer)
    Public Property Result As String
    Public Property MainUserID As String
End Class