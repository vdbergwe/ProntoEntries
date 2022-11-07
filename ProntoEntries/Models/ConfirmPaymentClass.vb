Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Partial Public Class ITN_Payload
    Public Property m_payment_id As String
    Public Property pf_payment_id As Integer
    Public Property payment_status As String
    Public Property item_name As String
    Public Property item_description As String
    Public Property amount_gross As Decimal
    Public Property amount_fee As Decimal
    Public Property amount_net As Decimal
    Public Property custom_str1 As String
    Public Property custom_str2 As String
    Public Property custom_str3 As String
    Public Property custom_str4 As String
    Public Property custom_str5 As String
    Public Property custom_int1 As Integer
    Public Property custom_int2 As Integer
    Public Property custom_int3 As Integer
    Public Property custom_int4 As Integer
    Public Property custom_int5 As Integer
    Public Property name_first As String
    Public Property name_last As String
    Public Property email_address As String
    Public Property merchant_id As Integer
    Public Property signature As String
End Class
