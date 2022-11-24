Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
<MetadataType(GetType(Meta_Participant))>
Partial Public Class Participant

End Class

Partial Public Class Meta_Participant
    Public Property ParticipantID As Integer
    <DisplayName("First Name")>
    <Required>
    Public Property FirstName As String
    <DisplayName("Middle Name")>
    Public Property MiddleNames As String
    <DisplayName("Surname")>
    <Required>
    Public Property LastName As String
    <DisplayName("ID/Passport")>
    <Required>
    Public Property IDNumber As String
    <DisplayName("Date of Birth")>
    <DataType(DataType.Date)>
    <DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)>
    <Required>
    Public Property DOB As Nullable(Of Date)
    <Required>
    Public Property Gender As String
    <DisplayName("Club Race Number")>
    Public Property RaceNumber As String
    <DisplayName("Email")>
    <DataType(DataType.EmailAddress)>
    <Required>
    Public Property EmailAddress As String
    <DisplayName("Medical Aid Name")>
    <Required>
    Public Property MedicalName As String
    <DisplayName("Medical Aid Number")>
    <Required>
    Public Property MedicalNumber As String
    <DisplayName("Emergency Contact Person")>
    <Required>
    Public Property EmergencyContact As String
    <DisplayName("Emergency Contact Number")>
    <Required>
    Public Property EmergencyNumber As String
    <DisplayName("Blood Type")>
    Public Property BoodType As String
    <Required>
    Public Property Allergies As String
    <DisplayName("Additional Information")>
    Public Property AdditionalInfo As String
    <DisplayName("House Doctor")>
    Public Property DoctorName As String
    <DisplayName("Doctor Number")>
    Public Property DoctorContact As String
    <DisplayName("Club Name")>
    Public Property Clubname As String
    <Required>
    Public Property Country As String
    <Required>
    Public Property Address As String
    <Required>
    Public Property City As String
    <Required>
    Public Property Province As String
    Public Property UserID As Nullable(Of Integer)
    <DisplayName("Notify Upcoming Events")>
    Public Property EventMailer As Nullable(Of Boolean)
    <DisplayName("Special Offers")>
    Public Property Offers As Nullable(Of Boolean)
End Class