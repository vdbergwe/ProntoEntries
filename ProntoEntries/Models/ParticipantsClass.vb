Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
<MetadataType(GetType(Meta_Participant))>
Partial Public Class Participant

End Class

Partial Public Class Meta_Participant
    Public Property ParticipantID As Integer
    <DisplayName("First Name")>
    Public Property FirstName As String
    <DisplayName("Middle Name")>
    Public Property MiddleNames As String
    <DisplayName("Surname")>
    Public Property LastName As String
    <DisplayName("ID/Passport")>
    Public Property IDNumber As String
    <DisplayName("Date of Birth")>
    <DataType(DataType.Date)>
    <DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)>
    Public Property DOB As Nullable(Of Date)
    <DisplayName("Race Number")>
    Public Property RaceNumber As String
    <DisplayName("Email")>
    <DataType(DataType.EmailAddress)>
    Public Property EmailAddress As String
    <DisplayName("Medical Aid Name")>
    Public Property MedicalName As String
    <DisplayName("Medical Aid Number")>
    Public Property MedicalNumber As String
    <DisplayName("Emergency Contact Person")>
    Public Property EmergencyContact As String
    <DisplayName("Emergency Contact Number")>
    Public Property EmergencyNumber As String
    <DisplayName("Bood Type")>
    Public Property BoodType As String
    Public Property Allergies As String
    <DisplayName("Additional Information")>
    Public Property AdditionalInfo As String
    <DisplayName("House Doctor")>
    Public Property DoctorName As String
    <DisplayName("Doctor Number")>
    Public Property DoctorContact As String
    <DisplayName("Club Name")>
    Public Property Clubname As String
    Public Property Country As String
    Public Property Address As String
    Public Property City As String
    Public Property Province As String
    Public Property UserID As Nullable(Of Integer)
    Public Property EventMailer As Nullable(Of Boolean)
    Public Property Offers As Nullable(Of Boolean)
End Class