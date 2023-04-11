Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ProntoEntries
Imports System.Security.Cryptography
Imports SelectPdf
Imports System.IO


Namespace Controllers
    Public Class EntriesController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        ' POST: Entries/ConfirmPayment
        Function ConfirmPayment(ByVal paymentdata As ITN_Payload, ByVal pftrans As pflog, ByVal entry As Entry) As ActionResult

            If db.pflogs.Where(Function(a) a.Pf_reference = paymentdata.pf_payment_id And a.Pf_status = "COMPLETE").Count() = 0 Then

                pftrans.Pf_status = paymentdata.payment_status
                pftrans.M_reference = paymentdata.m_payment_id
                pftrans.Pf_reference = paymentdata.pf_payment_id
                pftrans.Merchant_id = paymentdata.merchant_id
                pftrans.amount_gross = paymentdata.amount_gross
                pftrans.amount_fee = paymentdata.amount_fee
                pftrans.amount_net = paymentdata.amount_net
                pftrans.SaleID = db.Vouchers.Where(Function(a) a.Code = paymentdata.custom_str2).Select(Function(a) a.VoucherID).FirstOrDefault()
                pftrans.TAdate = Now()
                db.pflogs.Add(pftrans)
                db.SaveChanges()

                Dim SingleTransaction As Sale = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = paymentdata.custom_str1 And a.RaceID IsNot Nothing).FirstOrDefault()
                Dim Transaction = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = paymentdata.custom_str1)
                Dim MREF = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = paymentdata.custom_str1).Select(Function(b) b.M_reference).FirstOrDefault()
                If SingleTransaction Is Nothing Then
                    SingleTransaction = db.Sales.Where(Function(a) a.M_reference = MREF And a.UserID = paymentdata.custom_str1 And a.RaceID IsNot Nothing).FirstOrDefault()
                End If
                Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
                Dim OrgPassphrase = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantPassPhrase).FirstOrDefault()

                Dim RaceID = SingleTransaction.RaceID
                Dim Admin_rate = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_Rate).FirstOrDefault() / 100
                Dim EntriesTotal = 0.00
                Dim Voucher = paymentdata.custom_str2

                ViewBag.Total = 0.00
                For Each sale In Transaction
                    If sale.OptionID Is Nothing Then
                        ViewBag.Total += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                        EntriesTotal += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                    End If
                    If sale.RaceID Is Nothing Then
                        ViewBag.Total += db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                    End If
                Next

                Dim PCSAdminCharge = 0.00
                Dim PFAdminCharge = 0.00

                If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_ToClient).FirstOrDefault() = True Then
                    PCSAdminCharge = (EntriesTotal * Admin_rate)
                End If


                Dim VoucherValue As Decimal = 0
                ViewBag.VoucherValid = False
                If db.Vouchers.Where(Function(a) a.Code = Voucher And a.RaceID = RaceID).Select(Function(a) a.Status).FirstOrDefault() = "Active" Then
                    VoucherValue = db.Vouchers.Where(Function(a) a.Code = Voucher).Select(Function(a) a.Value).FirstOrDefault()
                    ViewBag.VoucherTotal = VoucherValue
                    ViewBag.VoucherValid = True
                    If VoucherValue < ViewBag.Total Then
                        If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                            Dim TAV As Decimal = ViewBag.Total - VoucherValue
                            PFAdminCharge = ((TAV + PCSAdminCharge + 2) / 0.965) - TAV - PCSAdminCharge
                        End If
                    End If
                Else
                    If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                        PFAdminCharge = ((ViewBag.Total + PCSAdminCharge + 2) / 0.965) - ViewBag.Total - PCSAdminCharge
                    End If
                End If

                'If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                '    PFAdminCharge = ((ViewBag.Total + PCSAdminCharge + 2) / 0.965) - ViewBag.Total - PCSAdminCharge
                'End If

                Dim AdminCharge = PCSAdminCharge + PFAdminCharge

                ViewBag.Total = Math.Round(ViewBag.Total + AdminCharge - VoucherValue, 2)

                If (paymentdata.amount_gross = ViewBag.Total) Then
                    Dim MD5String = "m_payment_id=" + System.Net.WebUtility.UrlEncode(paymentdata.m_payment_id) _
                                + "&pf_payment_id=" + System.Net.WebUtility.UrlEncode(paymentdata.pf_payment_id) _
                                + "&payment_status=" + System.Net.WebUtility.UrlEncode(paymentdata.payment_status) _
                                + "&item_name=" + System.Net.WebUtility.UrlEncode(paymentdata.item_name) _
                                + "&item_description=" + System.Net.WebUtility.UrlEncode(paymentdata.item_description) _
                                + "&amount_gross=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_gross) _
                                + "&amount_fee=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_fee) _
                                + "&amount_net=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_net) _
                                + "&custom_str1=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str1) _
                                + "&custom_str2=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str2) _
                                + "&custom_str3=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str3) _
                                + "&custom_str4=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str4) _
                                + "&custom_str5=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str5) _
                                + "&custom_int1=" _
                                + "&custom_int2=" _
                                + "&custom_int3=" _
                                + "&custom_int4=" _
                                + "&custom_int5=" _
                                + "&name_first=" + System.Net.WebUtility.UrlEncode(paymentdata.name_first) _
                                + "&name_last=" + System.Net.WebUtility.UrlEncode(paymentdata.name_last) _
                                + "&email_address=" + System.Net.WebUtility.UrlEncode(paymentdata.email_address) _
                                + "&merchant_id=" + System.Net.WebUtility.UrlEncode(paymentdata.merchant_id) _
                                + "&passphrase=" + System.Net.WebUtility.UrlEncode(OrgPassphrase)

                    Dim md5 As MD5 = MD5.Create()
                    Dim Bytes As Byte() = Encoding.ASCII.GetBytes(MD5String)
                    Dim hash As Byte() = md5.ComputeHash(Bytes)
                    Dim sBuilder As New StringBuilder()
                    For i As Integer = 0 To hash.Length - 1
                        sBuilder.Append(hash(i).ToString("x2"))
                    Next

                    If (paymentdata.signature = sBuilder.ToString()) Then
                        Dim result As Boolean
                        Dim SControl As New Controllers.SalesController()
                        Dim AllSales = db.Sales.Where(Function(a) a.M_reference = paymentdata.m_payment_id And a.Pf_reference Is Nothing)
                        Dim AllParticipantSales = db.Sales.Where(Function(a) a.M_reference = paymentdata.m_payment_id And a.RaceID IsNot Nothing And a.Pf_reference Is Nothing)
                        Dim VoucherUsed = db.Vouchers.Where(Function(a) a.Code = Voucher)

                        For Each sale In AllParticipantSales
                            entry.ParticipantID = sale.ParticipantID
                            entry.RaceID = sale.RaceID
                            entry.DivisionID = sale.DivisionID
                            entry.Amount = db.Divisions.Where(Function(a) a.DivisionID = entry.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                            entry.Status = "Paid"
                            entry.PaymentReference = paymentdata.m_payment_id.ToString()
                            entry.MainUserID = paymentdata.custom_str1
                            entry.PayFastReference = paymentdata.pf_payment_id.ToString()
                            entry.PayFastStatus = paymentdata.payment_status
                            entry.EntrySubmitDate = Now()
                            result = SControl.UpdateEntries(entry)
                        Next


                        For Each sale In AllSales
                            sale.Pf_reference = paymentdata.pf_payment_id
                            sale.Verified = 1
                            result = SControl.UpdateSales(sale)
                        Next

                        For Each item In VoucherUsed
                            item.Status = "Redeemed"
                            item.UsedBy = paymentdata.custom_str1
                            item.UsedDate = Now()
                            item.Pf_Reference = paymentdata.pf_payment_id.ToString()
                            item.M_Reference = paymentdata.m_payment_id.ToString()
                            result = SControl.UpdateVoucher(item)
                        Next

                        If result Then
                            Return New HttpStatusCodeResult(HttpStatusCode.OK)
                        End If

                        Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                    Else
                        Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                    End If
                Else
                    Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                End If
            End If
            Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)

        End Function

        Function VoucherPayment(ByVal Voucher As Integer?, ByVal entry As Entry) As ActionResult
            Dim SingleTransaction As Sale = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name And a.RaceID IsNot Nothing).FirstOrDefault()
            Dim Transaction = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name)
            Dim MREF = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name).Select(Function(b) b.M_reference).FirstOrDefault()
            If SingleTransaction Is Nothing Then
                SingleTransaction = db.Sales.Where(Function(a) a.M_reference = MREF And a.UserID = User.Identity.Name And a.RaceID IsNot Nothing).FirstOrDefault()
            End If
            Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
            Dim OrgPassphrase = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantPassPhrase).FirstOrDefault()

            Dim RaceID = SingleTransaction.RaceID
            Dim Admin_rate = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_Rate).FirstOrDefault() / 100
            Dim EntriesTotal = 0.00

            ViewBag.Total = 0.00
            For Each sale In Transaction
                If sale.OptionID Is Nothing Then
                    ViewBag.Total += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                    EntriesTotal += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                End If
                If sale.RaceID Is Nothing Then
                    ViewBag.Total += db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                End If
            Next

            Dim PCSAdminCharge = 0.00
            Dim PFAdminCharge = 0.00

            If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_ToClient).FirstOrDefault() = True Then
                PCSAdminCharge = (EntriesTotal * Admin_rate)
            End If


            Dim VoucherValue As Decimal = 0
            ViewBag.VoucherValid = False
            If db.Vouchers.Where(Function(a) a.Code = Voucher And a.RaceID = RaceID).Select(Function(a) a.Status).FirstOrDefault() = "Active" Then
                VoucherValue = db.Vouchers.Where(Function(a) a.Code = Voucher).Select(Function(a) a.Value).FirstOrDefault()
                ViewBag.VoucherTotal = VoucherValue
                ViewBag.VoucherValid = True
                If VoucherValue < ViewBag.Total Then
                    If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                        Dim TAV As Decimal = ViewBag.Total - VoucherValue
                        PFAdminCharge = ((TAV + PCSAdminCharge + 2) / 0.965) - TAV - PCSAdminCharge
                    End If
                End If
            Else
                If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                    PFAdminCharge = ((ViewBag.Total + PCSAdminCharge + 2) / 0.965) - ViewBag.Total - PCSAdminCharge
                End If
            End If

            Dim AdminCharge = PCSAdminCharge + PFAdminCharge

            ViewBag.Total = Math.Round(ViewBag.Total + AdminCharge - VoucherValue, 2)
            Dim MReference = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name).Select(Function(b) b.M_reference).FirstOrDefault()

            If db.Vouchers.Where(Function(a) a.Code = Voucher).Select(Function(b) b.Status).FirstOrDefault() = "Active" Then
                Dim result As Boolean
                Dim SControl As New Controllers.SalesController()
                Dim AllSales = db.Sales.Where(Function(a) a.M_reference = MReference And a.Pf_reference Is Nothing)
                Dim AllParticipantSales = db.Sales.Where(Function(a) a.M_reference = MReference And a.RaceID IsNot Nothing And a.Pf_reference Is Nothing)
                Dim VoucherUsed = db.Vouchers.Where(Function(a) a.Code = Voucher)

                For Each sale In AllParticipantSales
                    entry.ParticipantID = sale.ParticipantID
                    entry.RaceID = sale.RaceID
                    entry.DivisionID = sale.DivisionID
                    entry.Amount = db.Divisions.Where(Function(a) a.DivisionID = entry.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                    entry.Status = "Paid"
                    entry.PaymentReference = MReference
                    entry.MainUserID = User.Identity.Name
                    entry.PayFastReference = Voucher.ToString()
                    entry.PayFastStatus = "Voucher"
                    entry.EntrySubmitDate = Now()
                    result = SControl.UpdateEntries(entry)
                Next


                For Each sale In AllSales
                    sale.Pf_reference = Voucher
                    sale.Verified = 1
                    result = SControl.UpdateSales(sale)
                Next

                For Each item In VoucherUsed
                    item.Status = "Redeemed"
                    item.UsedBy = User.Identity.Name
                    item.UsedDate = Now()
                    item.Pf_Reference = "FullVoucher"
                    item.M_Reference = MReference
                    result = SControl.UpdateVoucher(item)
                Next

                If result Then
                    Return RedirectToAction("Index", "Entries")
                End If
                Return RedirectToAction("Cart", "Entries")


            End If


            Return RedirectToAction("Cart", "Entries")
        End Function

        Function SubmitToPayfast(Voucher As Integer?) As ActionResult
            Dim SingleTransaction As Sale = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name And a.RaceID IsNot Nothing).FirstOrDefault()
            Dim Transaction = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name)
            Dim MREF = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name).Select(Function(b) b.M_reference).FirstOrDefault()
            If SingleTransaction Is Nothing Then
                SingleTransaction = db.Sales.Where(Function(a) a.M_reference = MREF And a.UserID = User.Identity.Name And a.RaceID IsNot Nothing).FirstOrDefault()
            End If
            Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
            Dim OrgPassphrase = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantPassPhrase).FirstOrDefault()
            Dim hosturl = "https://entries.prontocs.co.za"
            'Dim hosturl = "https://9dbd-197-245-18-75.in.ngrok.io"

            Dim RaceID = SingleTransaction.RaceID

            Dim Admin_rate = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_Rate).FirstOrDefault() / 100
            Dim EntriesTotal = 0.00

            Dim Total = 0.00
            For Each sale In Transaction
                If sale.OptionID Is Nothing Then
                    Total += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                    EntriesTotal += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                End If
                If sale.RaceID Is Nothing Then
                    Total += db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                End If
            Next

            Dim PCSAdminCharge = 0.00
            Dim PFAdminCharge = 0.00


            If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_ToClient).FirstOrDefault() = True Then
                PCSAdminCharge = (EntriesTotal * Admin_rate)
            End If

            Dim VoucherValue As Decimal = 0
            ViewBag.VoucherValid = False
            If db.Vouchers.Where(Function(a) a.Code = Voucher And a.RaceID = RaceID).Select(Function(a) a.Status).FirstOrDefault() = "Active" Then
                VoucherValue = db.Vouchers.Where(Function(a) a.Code = Voucher).Select(Function(a) a.Value).FirstOrDefault()
                ViewBag.VoucherTotal = VoucherValue
                ViewBag.VoucherValid = True
                If VoucherValue < Total Then
                    If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                        Dim TAV As Decimal = Total - VoucherValue
                        PFAdminCharge = ((TAV + PCSAdminCharge + 2) / 0.965) - TAV - PCSAdminCharge
                    End If
                End If
            Else
                If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                    PFAdminCharge = ((Total + PCSAdminCharge + 2) / 0.965) - Total - PCSAdminCharge
                End If
            End If

            Dim AdminCharge = PCSAdminCharge + PFAdminCharge

            Total = Math.Round(Total + AdminCharge - VoucherValue, 2)

            ViewBag.MReference = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name).Select(Function(b) b.M_reference).FirstOrDefault()
            ViewBag.EmailAddress = User.Identity.Name
            ViewBag.Emailconfirmation = "1"
            ViewBag.MerchantID = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantID).FirstOrDefault()
            ViewBag.Merchant_key = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantKey).FirstOrDefault()
            'ViewBag.MerchantID = "10028506"
            'ViewBag.Merchant_key = "ds0rpjbz65yub"
            ViewBag.ReturnURL = hosturl + "/Entries/Index"
            ViewBag.CancelURL = hosturl + "/Entries/Cart"
            ViewBag.NotifyURL = hosturl + "/Entries/Confirmpayment"
            ViewBag.item_name = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.RaceName).FirstOrDefault()

            Dim TransactionString = "merchant_id=" + System.Net.WebUtility.UrlEncode(ViewBag.MerchantID) + "&merchant_key=" + System.Net.WebUtility.UrlEncode(ViewBag.Merchant_key) _
                 + "&return_url=" + System.Net.WebUtility.UrlEncode(ViewBag.ReturnURL) + "&cancel_url=" + System.Net.WebUtility.UrlEncode(ViewBag.CancelURL) _
                 + "&notify_url=" + System.Net.WebUtility.UrlEncode(ViewBag.NotifyURL) + "&m_payment_id=" + System.Net.WebUtility.UrlEncode(ViewBag.MReference) _
                 + "&amount=" + System.Net.WebUtility.UrlEncode(Total) _
                 + "&item_name=" + System.Net.WebUtility.UrlEncode(ViewBag.item_name.ToString) + "&custom_str1=" _
                 + System.Net.WebUtility.UrlEncode(ViewBag.EmailAddress) + "&custom_str2=" + System.Net.WebUtility.UrlEncode(Voucher.ToString())

            Dim Constring = TransactionString + "&passphrase=" + System.Net.WebUtility.UrlEncode(OrgPassphrase)

            Dim md5 As MD5 = MD5.Create()
            Dim Bytes As Byte() = Encoding.ASCII.GetBytes(Constring)
            Dim hash As Byte() = md5.ComputeHash(Bytes)
            Dim sBuilder As New StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                sBuilder.Append(hash(i).ToString("x2"))
            Next

            ViewBag.Signature = sBuilder.ToString

            TransactionString = TransactionString + "&" + "signature=" + ViewBag.Signature

            Return Redirect("https://www.payfast.co.za/eng/process?" + TransactionString)
        End Function

        Function Confirmadmin(ByVal paymentdata As ITN_Payload, ByVal pftrans As pflog, ByVal entry As Entry, NewVoucher As Voucher) As ActionResult

            If db.pflogs.Where(Function(a) a.Pf_reference = paymentdata.pf_payment_id And a.Pf_status = "COMPLETE").Count() = 0 Then

                pftrans.Pf_status = paymentdata.payment_status
                pftrans.M_reference = paymentdata.m_payment_id
                pftrans.Pf_reference = paymentdata.pf_payment_id
                pftrans.Merchant_id = paymentdata.merchant_id
                pftrans.amount_gross = paymentdata.amount_gross
                pftrans.amount_fee = paymentdata.amount_fee
                pftrans.amount_net = paymentdata.amount_net
                pftrans.SaleID = 9999999
                pftrans.TAdate = Now()
                db.pflogs.Add(pftrans)
                db.SaveChanges()

                entry = db.Entries.Where(Function(a) a.EntryID = paymentdata.custom_str2).FirstOrDefault()
                Dim SingleTransaction As Sale = db.Sales.Where(Function(a) a.Pf_reference = entry.PayFastReference And a.UserID = paymentdata.custom_str1 And a.RaceID = entry.RaceID).FirstOrDefault()
                Dim Transaction = db.Sales.Where(Function(a) a.Pf_reference = entry.PayFastReference And a.UserID = paymentdata.custom_str1)
                Dim MREF = entry.PayFastReference

                Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
                Dim OrgPassphrase = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantPassPhrase).FirstOrDefault()

                Dim RaceID = SingleTransaction.RaceID
                Dim Admin_rate = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_Rate).FirstOrDefault() / 100
                Dim EntriesTotal = 0.00

                ViewBag.Total = 0.00
                For Each sale In Transaction
                    If sale.OptionID Is Nothing Then
                        ViewBag.Total += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                        EntriesTotal += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                    End If
                    If sale.RaceID Is Nothing Then
                        ViewBag.Total += db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                    End If
                Next

                Dim PCSAdminCharge = 0.00

                If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_ToClient).FirstOrDefault() = True Then
                    PCSAdminCharge = (EntriesTotal * Admin_rate)
                End If

                Dim AdminCharge = PCSAdminCharge

                ViewBag.Total = Math.Round(ViewBag.Total + AdminCharge, 2)
                Dim VoucherValue As Decimal = ViewBag.Total

                Dim AdminFee = db.RaceEvents.Where(Function(a) a.RaceID = entry.RaceID).Select(Function(b) b.AdminCharge).FirstOrDefault()

                If (paymentdata.amount_gross = paymentdata.custom_str4) Then
                    Dim MD5String = "m_payment_id=" + System.Net.WebUtility.UrlEncode(paymentdata.m_payment_id) _
                                + "&pf_payment_id=" + System.Net.WebUtility.UrlEncode(paymentdata.pf_payment_id) _
                                + "&payment_status=" + System.Net.WebUtility.UrlEncode(paymentdata.payment_status) _
                                + "&item_name=" + System.Net.WebUtility.UrlEncode(paymentdata.item_name) _
                                + "&item_description=" + System.Net.WebUtility.UrlEncode(paymentdata.item_description) _
                                + "&amount_gross=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_gross) _
                                + "&amount_fee=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_fee) _
                                + "&amount_net=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_net) _
                                + "&custom_str1=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str1) _
                                + "&custom_str2=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str2) _
                                + "&custom_str3=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str3) _
                                + "&custom_str4=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str4) _
                                + "&custom_str5=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str5) _
                                + "&custom_int1=" _
                                + "&custom_int2=" _
                                + "&custom_int3=" _
                                + "&custom_int4=" _
                                + "&custom_int5=" _
                                + "&name_first=" + System.Net.WebUtility.UrlEncode(paymentdata.name_first) _
                                + "&name_last=" + System.Net.WebUtility.UrlEncode(paymentdata.name_last) _
                                + "&email_address=" + System.Net.WebUtility.UrlEncode(paymentdata.email_address) _
                                + "&merchant_id=" + System.Net.WebUtility.UrlEncode(paymentdata.merchant_id) _
                                + "&passphrase=" + System.Net.WebUtility.UrlEncode(OrgPassphrase)

                    Dim md5 As MD5 = MD5.Create()
                    Dim Bytes As Byte() = Encoding.ASCII.GetBytes(MD5String)
                    Dim hash As Byte() = md5.ComputeHash(Bytes)
                    Dim sBuilder As New StringBuilder()
                    For i As Integer = 0 To hash.Length - 1
                        sBuilder.Append(hash(i).ToString("x2"))
                    Next

                    If (paymentdata.signature = sBuilder.ToString()) Then
                        Dim result As Boolean

                        If paymentdata.custom_str3 = "1" Then

                            Dim EntryID As Integer = paymentdata.custom_str2
                            NewVoucher.Value = VoucherValue
                            NewVoucher.Pf_Reference = paymentdata.pf_payment_id
                            NewVoucher.M_Reference = paymentdata.m_payment_id
                            Dim Code = IssueVoucher(NewVoucher, RaceID, paymentdata.custom_str1)
                            Dim Vouchercode As Integer = Int32.Parse(Code)
                            result = UpdateSale(EntryID, Nothing)
                            result = UpdateEntry(EntryID, Vouchercode, paymentdata.pf_payment_id, Nothing)
                        End If

                        If paymentdata.custom_str3 = "2" Then
                            Dim EntryID As Integer = paymentdata.custom_str2
                            Dim DivisionID As Integer = paymentdata.custom_str5
                            result = UpdateSale(EntryID, DivisionID)
                            result = UpdateEntry(EntryID, Nothing, paymentdata.pf_payment_id, DivisionID)
                        End If

                        If result Then
                            Return New HttpStatusCodeResult(HttpStatusCode.OK)
                        End If

                        Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                    Else
                        Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                    End If
                Else
                    Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                End If
            End If
            Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)

        End Function

        Function UpdateEntry(EntryId As Integer?, VoucherID As Integer?, PFRef As Integer?, DivisionID As Integer?)
            Dim result As Boolean = False
            Dim Entry As Entry = db.Entries.Where(Function(a) a.EntryID = EntryId).FirstOrDefault()

            If DivisionID IsNot Nothing Then
                Entry.DistanceChange = True
                Entry.DivisionID = DivisionID
                Entry.Amount = db.Divisions.Where(Function(a) a.DivisionID = DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                Entry.ChangePaymentRef = PFRef.ToString()
                db.Entry(Entry).State = EntityState.Modified
                db.SaveChanges()
                result = True
            Else
                Entry.Status = "Substitute"
                Entry.TransferID = VoucherID
                Entry.ChangePaymentRef = PFRef.ToString()
                db.Entry(Entry).State = EntityState.Modified
                db.SaveChanges()
                result = True
            End If

            Return (result)
        End Function

        Function UpdateSale(EntryId As Integer?, DivisionID As Integer?)
            Dim result As Boolean = False
            Dim ParticipantID = db.Entries.Where(Function(a) a.EntryID = EntryId).Select(Function(b) b.ParticipantID).FirstOrDefault()
            Dim Mref = db.Entries.Where(Function(a) a.EntryID = EntryId).Select(Function(b) b.PaymentReference).FirstOrDefault()


            If DivisionID IsNot Nothing Then
                Dim sale As Sale = db.Sales.Where(Function(a) a.ParticipantID = ParticipantID And a.M_reference = Mref And a.RaceID IsNot Nothing).FirstOrDefault()
                sale.DivisionID = DivisionID
                db.Entry(sale).State = EntityState.Modified
                db.SaveChanges()
                result = True
            Else
                Dim Sales = db.Sales.Where(Function(a) a.ParticipantID = ParticipantID And a.M_reference = Mref)
                For Each UpdateSale In Sales
                    UpdateSale.Pf_reference = Nothing
                    UpdateSale.Verified = Nothing
                    If ModelState.IsValid Then
                        db.Entry(UpdateSale).State = EntityState.Modified
                        db.SaveChanges()
                        result = True
                    Else
                        result = False
                    End If
                Next
            End If


            Return (result)
        End Function

        Function IssueVoucher(ByVal voucher As Voucher, RaceID As Integer?, UserIdentity As String)
            Dim VControl As New Controllers.VouchersController()
            voucher.Code = "22" + VControl.GenerateVoucherCode(7)

            Dim flag As Boolean = False

            While flag = False
                If db.Vouchers.Where(Function(a) a.Code = voucher.Code).Count() = 0 Then
                    flag = True
                Else
                    voucher.Code = "22" + VControl.GenerateVoucherCode(7)
                End If
            End While

            voucher.RaceID = RaceID
            voucher.IssuedBy = UserIdentity
            voucher.Date = Now()
            voucher.Status = "Active"

            db.Vouchers.Add(voucher)
            db.SaveChanges()

            Return (voucher.Code)
        End Function

        Function AdminToPayfast(Id As Integer?, Type As Integer?) As ActionResult
            Dim entry As Entry = db.Entries.Where(Function(a) a.EntryID = Id).FirstOrDefault()
            Dim SingleTransaction As Sale = db.Sales.Where(Function(a) a.Pf_reference = entry.PayFastReference And a.UserID = User.Identity.Name And a.RaceID = entry.RaceID).FirstOrDefault()
            Dim Transaction = db.Sales.Where(Function(a) a.Pf_reference = entry.PayFastReference And a.ParticipantID = entry.ParticipantID)
            Dim MREF = SingleTransaction.M_reference

            Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
            Dim OrgPassphrase = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantPassPhrase).FirstOrDefault()
            Dim hosturl = "https://entries.prontocs.co.za"
            'Dim hosturl = "https://ed66-197-245-32-250.ngrok-free.app"

            Dim RaceID = SingleTransaction.RaceID

            Dim Admin_rate = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_Rate).FirstOrDefault() / 100
            Dim EntriesTotal = 0.00

            Dim Total = 0.00
            For Each sale In Transaction
                If sale.OptionID Is Nothing Then
                    Total += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                    EntriesTotal += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                End If
                If sale.RaceID Is Nothing Then
                    Total += db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                End If
            Next

            Dim PCSAdminCharge = 0.00

            If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_ToClient).FirstOrDefault() = True Then
                PCSAdminCharge = (EntriesTotal * Admin_rate)
            End If

            Dim AdminCharge = PCSAdminCharge

            Total = Math.Round(Total + AdminCharge, 2)

            Dim AdminFee = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.AdminCharge).FirstOrDefault()

            ViewBag.MReference = SingleTransaction.M_reference
            ViewBag.EmailAddress = User.Identity.Name
            ViewBag.Emailconfirmation = "1"
            ViewBag.MerchantID = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantID).FirstOrDefault()
            ViewBag.Merchant_key = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantKey).FirstOrDefault()
            'ViewBag.MerchantID = "10028506"
            'ViewBag.Merchant_key = "ds0rpjbz65yub"
            ViewBag.ReturnURL = hosturl + "/Entries/Index"
            ViewBag.CancelURL = hosturl + "/Entries/Index"
            ViewBag.NotifyURL = hosturl + "/Entries/Confirmadmin"
            ViewBag.item_name = "Substitution"

            Dim TransactionString = "merchant_id=" + System.Net.WebUtility.UrlEncode(ViewBag.MerchantID) + "&merchant_key=" + System.Net.WebUtility.UrlEncode(ViewBag.Merchant_key) _
                 + "&return_url=" + System.Net.WebUtility.UrlEncode(ViewBag.ReturnURL) + "&cancel_url=" + System.Net.WebUtility.UrlEncode(ViewBag.CancelURL) _
                 + "&notify_url=" + System.Net.WebUtility.UrlEncode(ViewBag.NotifyURL) + "&m_payment_id=" + System.Net.WebUtility.UrlEncode(ViewBag.MReference) _
                 + "&amount=" + System.Net.WebUtility.UrlEncode(AdminFee) _
                 + "&item_name=" + System.Net.WebUtility.UrlEncode(ViewBag.item_name.ToString) + "&custom_str1=" _
                 + System.Net.WebUtility.UrlEncode(ViewBag.EmailAddress) + "&custom_str2=" + System.Net.WebUtility.UrlEncode(Id.ToString()) _
                 + "&custom_str3=" + System.Net.WebUtility.UrlEncode(Type.ToString())

            Dim Constring = TransactionString + "&passphrase=" + System.Net.WebUtility.UrlEncode(OrgPassphrase)

            Dim md5 As MD5 = MD5.Create()
            Dim Bytes As Byte() = Encoding.ASCII.GetBytes(Constring)
            Dim hash As Byte() = md5.ComputeHash(Bytes)
            Dim sBuilder As New StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                sBuilder.Append(hash(i).ToString("x2"))
            Next

            ViewBag.Signature = sBuilder.ToString

            TransactionString = TransactionString + "&" + "signature=" + ViewBag.Signature

            Return Redirect("https://www.payfast.co.za/eng/process?" + TransactionString)
        End Function

        Function AdminUpgradeToPayfast(Id As Integer?, DivisionID As Integer?, Price As Double?, Type As Integer?) As ActionResult
            Dim entry As Entry = db.Entries.Where(Function(a) a.EntryID = Id).FirstOrDefault()
            Dim SingleTransaction As Sale = db.Sales.Where(Function(a) a.Pf_reference = entry.PayFastReference And a.UserID = User.Identity.Name And a.RaceID = entry.RaceID).FirstOrDefault()
            Dim Transaction = db.Sales.Where(Function(a) a.Pf_reference = entry.PayFastReference And a.ParticipantID = entry.ParticipantID)
            Dim MREF = SingleTransaction.M_reference

            Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
            Dim OrgPassphrase = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantPassPhrase).FirstOrDefault()
            Dim hosturl = "https://entries.prontocs.co.za"
            'Dim hosturl = "https://ed66-197-245-32-250.ngrok-free.app"

            Dim RaceID = SingleTransaction.RaceID

            Dim Admin_rate = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_Rate).FirstOrDefault() / 100

            Dim PCSAdminCharge = 0.00
            Dim Total = 0.00
            Total = Price

            If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_ToClient).FirstOrDefault() = True Then
                PCSAdminCharge = (Price * Admin_rate)
            End If

            Dim AdminCharge = PCSAdminCharge

            Dim AdminFee = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.AdminCharge).FirstOrDefault()
            Total = Total + AdminFee

            Total = Math.Round(Total + AdminCharge, 2)

            ViewBag.MReference = SingleTransaction.M_reference
            ViewBag.EmailAddress = User.Identity.Name
            ViewBag.Emailconfirmation = "1"
            ViewBag.MerchantID = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantID).FirstOrDefault()
            ViewBag.Merchant_key = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantKey).FirstOrDefault()
            'ViewBag.MerchantID = "10028506"
            'ViewBag.Merchant_key = "ds0rpjbz65yub"
            ViewBag.ReturnURL = hosturl + "/Entries/Index"
            ViewBag.CancelURL = hosturl + "/Entries/Index"
            ViewBag.NotifyURL = hosturl + "/Entries/Confirmadmin"
            ViewBag.item_name = "Distance_Change"

            Dim TransactionString = "merchant_id=" + System.Net.WebUtility.UrlEncode(ViewBag.MerchantID) + "&merchant_key=" + System.Net.WebUtility.UrlEncode(ViewBag.Merchant_key) _
                 + "&return_url=" + System.Net.WebUtility.UrlEncode(ViewBag.ReturnURL) + "&cancel_url=" + System.Net.WebUtility.UrlEncode(ViewBag.CancelURL) _
                 + "&notify_url=" + System.Net.WebUtility.UrlEncode(ViewBag.NotifyURL) + "&m_payment_id=" + System.Net.WebUtility.UrlEncode(ViewBag.MReference) _
                 + "&amount=" + System.Net.WebUtility.UrlEncode(Total) _
                 + "&item_name=" + System.Net.WebUtility.UrlEncode(ViewBag.item_name.ToString) + "&custom_str1=" _
                 + System.Net.WebUtility.UrlEncode(ViewBag.EmailAddress) + "&custom_str2=" + System.Net.WebUtility.UrlEncode(Id.ToString()) _
                 + "&custom_str3=" + System.Net.WebUtility.UrlEncode(Type.ToString()) + "&custom_str4=" + System.Net.WebUtility.UrlEncode(Total.ToString()) _
                 + "&custom_str5=" + System.Net.WebUtility.UrlEncode(DivisionID.ToString())

            Dim Constring = TransactionString + "&passphrase=" + System.Net.WebUtility.UrlEncode(OrgPassphrase)

            Dim md5 As MD5 = MD5.Create()
            Dim Bytes As Byte() = Encoding.ASCII.GetBytes(Constring)
            Dim hash As Byte() = md5.ComputeHash(Bytes)
            Dim sBuilder As New StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                sBuilder.Append(hash(i).ToString("x2"))
            Next

            ViewBag.Signature = sBuilder.ToString

            TransactionString = TransactionString + "&" + "signature=" + ViewBag.Signature

            Return Redirect("https://www.payfast.co.za/eng/process?" + TransactionString)
        End Function

        ' GET: Entries/Cart
        <Authorize>
        Function Cart(ByVal id As Integer?, ByVal DivisionSelect As Integer?, ByVal Voucher As String) As ActionResult

            Dim CartContent = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name).OrderBy(Function(b) b.ParticipantID).ThenByDescending(Function(b) b.RaceID)
            Dim ParticipantsInCart = CartContent.Select(Function(a) a.ParticipantID).Distinct().ToList()
            ViewBag.UniqueP = CartContent.Select(Function(a) a.ParticipantID).Distinct().ToList()
            ViewBag.VoucherCode = Voucher

            Dim ShopSaleFlag As Boolean = False

            For Each Participant In ParticipantsInCart
                Dim CheckEntry = CartContent.Where(Function(a) a.ParticipantID = Participant And a.RaceID IsNot Nothing).Count()
                Dim MRefCheck = CartContent.Where(Function(a) a.ParticipantID = Participant).Select(Function(b) b.M_reference).FirstOrDefault()
                Dim PEntryCheck = db.Sales.Where(Function(a) a.M_reference = MRefCheck And a.Pf_reference IsNot Nothing).Count()
                If PEntryCheck > 0 Then
                    ShopSaleFlag = True
                End If
                If CheckEntry = 0 And PEntryCheck = 0 Then
                    Dim DeleteSales = db.Sales.Where(Function(a) a.ParticipantID = Participant And a.Pf_reference Is Nothing And a.UserID = User.Identity.Name).ToList()
                    For Each Tsale In DeleteSales
                        Dim sale As Sale = db.Sales.Find(Tsale.SaleID)
                        db.Sales.Remove(sale)
                        db.SaveChanges()
                    Next
                End If
            Next
            Dim RaceID As Integer?
            If ShopSaleFlag = False Then
                RaceID = CartContent.Where(Function(a) a.RaceID IsNot Nothing).Select(Function(b) b.RaceID).FirstOrDefault()
            Else
                RaceID = db.Sales.Where(Function(a) a.M_reference = CartContent.Where(Function(b) b.M_reference IsNot Nothing).Select(Function(b) b.M_reference).FirstOrDefault() And a.RaceID IsNot Nothing).Select(Function(c) c.RaceID).FirstOrDefault()
            End If


            Dim Admin_rate = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_Rate).FirstOrDefault() / 100
            Dim EntriesTotal = 0.00

            ViewBag.Total = 0
            For Each sale In CartContent
                If sale.OptionID Is Nothing Then
                    ViewBag.Total = ViewBag.Total + db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                    EntriesTotal += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                End If

                If sale.RaceID Is Nothing Then
                    ViewBag.Total = ViewBag.Total + db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                End If
            Next

            Dim PCSAdminCharge = 0.00
            Dim PFAdminCharge = 0.00

            If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.Admin_ToClient).FirstOrDefault() = True Then
                PCSAdminCharge = (EntriesTotal * Admin_rate)
            End If
            Dim VoucherValue As Decimal = 0
            ViewBag.VoucherValid = False
            If db.Vouchers.Where(Function(a) a.Code = Voucher And a.RaceID = RaceID).Select(Function(a) a.Status).FirstOrDefault() = "Active" Then
                VoucherValue = db.Vouchers.Where(Function(a) a.Code = Voucher).Select(Function(a) a.Value).FirstOrDefault()
                ViewBag.VoucherTotal = VoucherValue
                ViewBag.VoucherValid = True
                If VoucherValue < ViewBag.Total Then
                    If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                        Dim TAV As Decimal = ViewBag.Total - VoucherValue
                        PFAdminCharge = ((TAV + PCSAdminCharge + 2) / 0.965) - TAV - PCSAdminCharge
                    End If
                End If
            Else
                If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.PF_ToClient).FirstOrDefault() = True Then
                    PFAdminCharge = ((ViewBag.Total + PCSAdminCharge + 2) / 0.965) - ViewBag.Total - PCSAdminCharge
                End If
            End If

            Dim AdminCharge = PCSAdminCharge + PFAdminCharge

            ViewBag.AdminCharge = Math.Round(AdminCharge, 2)
            ViewBag.Total = Math.Round(ViewBag.Total + AdminCharge, 2)
            ViewBag.Outstanding = ViewBag.Total - ViewBag.VoucherTotal

            Return View(CartContent.ToList())
        End Function


        ' GET: Entries/Create
        <Authorize>
        Function NewEntry(ByVal id As Integer?, ByVal DivisionSelect As Decimal?) As ActionResult
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(id)
            Dim Organiser As Organiser = db.Organisers.Find(raceEvent.OrgID)
            ViewBag.Background = raceEvent.Background
            ViewBag.OrgImage = Organiser.Image
            ViewBag.DivisionSelect = DivisionSelect
            Dim Distances = db.Divisions.Where(Function(a) a.RaceID = id).Select(Function(b) b.Distance).Distinct()
            ViewBag.Distance = New SelectList(Distances, Nothing, Nothing, DivisionSelect)
            'ViewBag.Distance = New SelectList(db.Divisions.Where(Function(a) a.RaceID = id), "Distance", "Distance", DivisionSelect)

            ViewBag.TotalEntries = db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing And a.RaceID IsNot Nothing).Count()


            ViewBag.DivisionID = New SelectList(db.Divisions.Where(Function(a) a.RaceID = id), "DivisionID", "Category", DivisionSelect)
            ViewBag.Background = raceEvent.Background
            ViewBag.OrgImage = Organiser.Image
            ViewBag.RaceID = id
            ViewBag.RaceName = raceEvent.RaceName
            ViewBag.Background = raceEvent.Background
            ViewBag.OrgID = Organiser.OrgName
            ViewBag.OrgImage = Organiser.Image
            ViewBag.RaceDate = raceEvent.RaceDate.Value.ToString("dddd, dd MMMM yyyy")
            Return View()
        End Function

        <Authorize>
        Function Addtocart(ByVal sale As Sale, ByVal id As Integer?, ByVal RaceID As Integer?, ByVal Distance As Decimal?) As ActionResult
            Dim OrderNumber As Integer
            Dim RaceDate As Date = db.RaceEvents.Where(Function(c) c.RaceID = RaceID).Select(Function(d) d.RaceDate).FirstOrDefault()
            Dim ParticipantDOB As Date = db.Participants.Where(Function(a) a.ParticipantID = id).Select(Function(b) b.DOB).FirstOrDefault()
            Dim Age As TimeSpan = RaceDate - ParticipantDOB
            Dim AgeInt As Decimal = Age.TotalDays / 365

            Dim DivisionID = db.Divisions.Where(Function(a) a.RaceID = RaceID And a.MinAge < AgeInt And a.MaxAge > AgeInt And a.Distance = Distance).Select(Function(b) b.DivisionID).FirstOrDefault()

            If db.Sales.Where(Function(a) a.ParticipantID = id And a.RaceID = RaceID And a.Verified = True).Count > 0 Then
                Return RedirectToAction("Index", "Entries")

            Else
                If DivisionID > 0 Then
                    sale.ParticipantID = id
                    sale.RaceID = RaceID
                    sale.DivisionID = DivisionID
                    Dim Amount = db.Divisions.Where(Function(a) a.DivisionID = DivisionID).Select(Function(a) a.Price).FirstOrDefault
                    Dim Status = "UnPaid"
                    sale.UserID = User.Identity.Name
                    sale.TandC = True
                    sale.Indemnity = True
                    sale.SaleDate = Now()

                    If (db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing).Count() > 0) Then
                        sale.M_reference = db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing).Select(Function(a) a.M_reference).FirstOrDefault()
                    Else
                        If IsNothing(db.Sales.Max(Function(a) a.M_reference)) Then
                            sale.M_reference = 1
                        Else
                            OrderNumber = db.Sales.Max(Function(a) a.M_reference)
                            sale.M_reference = OrderNumber + 1
                        End If
                    End If
                    If ModelState.IsValid Then
                        db.Sales.Add(sale)
                        db.SaveChanges()
                        '@Html.ActionLink("Enter Now", "NewEntry", "Entries", New With {.id = Model.RaceID}, New With {.class = "btnEntryLink"})
                        Return RedirectToAction("NewEntry", "Entries", New With {.id = RaceID, .DivisionSelect = DivisionID})
                    End If
                End If

                Return RedirectToAction("NewEntry", "Entries", New With {.id = RaceID, .DivisionSelect = DivisionID, .alert = "No Suitable Age Category for selected Participant"})
            End If

            Return RedirectToAction("NewEntry", "Entries", New With {.id = RaceID, .DivisionSelect = DivisionID, .alert = "No Suitable Age Category for selected Participant"})

        End Function

        <Authorize>
        Function VerifyEntry(ByVal sale As Sale, ByVal Id As Integer?, ByVal RaceID1 As Integer?, ByVal DivisionID1 As Decimal?, ByVal OptionID1 As Integer?, ByVal ItemID As Integer?) As ActionResult
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(RaceID1)
            Dim Organiser As Organiser = db.Organisers.Find(raceEvent.OrgID)
            Dim OrderNumber As Integer

            Dim RaceDate As Date = db.RaceEvents.Where(Function(c) c.RaceID = RaceID1).Select(Function(d) d.RaceDate).FirstOrDefault()
            Dim ParticipantDOB As Date = db.Participants.Where(Function(a) a.ParticipantID = Id).Select(Function(b) b.DOB).FirstOrDefault()
            Dim Age As TimeSpan = RaceDate - ParticipantDOB
            Dim AgeInt As Decimal = Age.TotalDays / 365

            Dim DivisionID = db.Divisions.Where(Function(a) a.RaceID = RaceID1 And a.MinAge <= AgeInt And a.MaxAge >= AgeInt And a.Distance = DivisionID1).Select(Function(b) b.DivisionID).FirstOrDefault()

            ViewBag.DivisionCheck = DivisionID

            ViewBag.Background = raceEvent.Background
            ViewBag.OrgImage = Organiser.Image

            ViewBag.Indemnity = db.RaceEvents.Where(Function(a) a.RaceID = RaceID1).Select(Function(b) b.Indemnity).FirstOrDefault()
            ViewBag.TandC = db.RaceEvents.Where(Function(a) a.RaceID = RaceID1).Select(Function(b) b.TandC).FirstOrDefault()
            ViewBag.Racename = db.RaceEvents.Where(Function(a) a.RaceID = RaceID1).Select(Function(b) b.RaceName).FirstOrDefault()
            ViewBag.ParticipantID = Id
            ViewBag.RaceID = RaceID1
            ViewBag.DivisionID = DivisionID1
            ViewBag.OptionID = OptionID1
            ViewBag.ItemID = ItemID

            Dim holding = db.Sales.Where(Function(a) a.ParticipantID = Id And a.Pf_reference Is Nothing)
            Dim AddItems = db.AddonItems.Where(Function(a) a.RaceID = RaceID1 And Not holding.Any(Function(b) b.ItemID = a.ItemID)).OrderBy(Function(b) b.Name)

            If (db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing).Count() > 0) Then
                sale.M_reference = db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing).Select(Function(a) a.M_reference).FirstOrDefault()
            Else
                If IsNothing(db.Sales.Max(Function(a) a.M_reference)) Then
                    sale.M_reference = 1
                Else
                    OrderNumber = db.Sales.Max(Function(a) a.M_reference)
                    sale.M_reference = OrderNumber + 1
                End If
            End If

            Dim Flag As Boolean


            If db.Sales.Where(Function(a) a.ParticipantID = Id And a.ItemID = ItemID).Count = 0 Then
                Flag = True
            End If

            If OptionID1 IsNot Nothing And Flag = True Then
                sale.ParticipantID = Id
                sale.ItemID = ItemID
                sale.OptionID = OptionID1
                sale.UserID = User.Identity.Name
                sale.SaleDate = Now()
                If ModelState.IsValid Then
                    db.Sales.Add(sale)
                    db.SaveChanges()
                    Return RedirectToAction("VerifyEntry", "Entries", New With {.id = Id, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID})
                End If
                ViewBag.OptionID = Nothing
            End If

            ViewBag.Required = AddItems.Where(Function(a) a.Required = True).Count()

            Return View(AddItems.ToList())
        End Function

        <Authorize>
        Function get_CartAmount(Id As Integer?, ByVal OptionID As Integer?) As ActionResult
            If Id Is Nothing Then
                ViewBag.CartAmount = db.AddonOptions.Where(Function(a) a.OptionID = OptionID).Select(Function(b) b.Amount).FirstOrDefault()
            Else
                ViewBag.CartAmount = db.Divisions.Where(Function(a) a.DivisionID = Id).Select(Function(b) b.Price).FirstOrDefault()
            End If
            Return PartialView()
        End Function

        <Authorize>
        Function get_AddonOptionlist(Id As Integer?, ByVal RaceID As Integer?, ByVal DivisionID1 As Decimal?, ByVal OptionID As Integer?, ByVal ParticipantID As Integer?) As ActionResult
            Dim Optionlist = db.AddonOptions.Where(Function(a) a.ItemID = Id And a.StopDate > Now()).OrderBy(Function(b) b.Size)
            ViewBag.ParticipantID = ParticipantID
            ViewBag.RaceID = RaceID
            ViewBag.DivisionID = DivisionID1
            ViewBag.OptionID = OptionID
            ViewBag.ItemID = Id

            Return PartialView(Optionlist.ToList())
        End Function

        Function Get_DivisionDistance(Id As Integer?) As ActionResult
            If Id Is Nothing Then
                ViewBag.DivisionDistance = db.Divisions.Where(Function(a) a.DivisionID = Id).Select(Function(b) b.Distance).FirstOrDefault().ToString()
            Else
                ViewBag.DivisionDistance = db.Divisions.Where(Function(a) a.DivisionID = Id).Select(Function(b) b.Distance).FirstOrDefault().ToString() + " Km"
            End If
            Return PartialView()
        End Function

        Function Get_DivisionName(Id As Integer?) As ActionResult
            ViewBag.DivisionName = db.Divisions.Where(Function(a) a.DivisionID = Id).Select(Function(b) b.Category).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_Distance(Id As Integer?) As ActionResult
            ViewBag.Distance = db.Divisions.Where(Function(a) a.DivisionID = Id).Select(Function(b) b.Distance).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_ParticipantName(Id As Integer?) As ActionResult
            ViewBag.ParticipantName = db.Participants.Where(Function(a) a.ParticipantID = Id).Select(Function(b) b.FirstName).FirstOrDefault() + " " +
                db.Participants.Where(Function(a) a.ParticipantID = Id).Select(Function(b) b.LastName).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_RaceName(Id As Integer?) As ActionResult
            ViewBag.RaceName = db.RaceEvents.Where(Function(a) a.RaceID = Id).Select(Function(b) b.RaceName).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_ParticipantFirstName(Id As Integer?) As ActionResult
            ViewBag.ParticipantFirstName = db.Participants.Where(Function(a) a.ParticipantID = Id).Select(Function(b) b.FirstName).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_ParticipantLastName(Id As Integer?) As ActionResult
            ViewBag.ParticipantLastName = db.Participants.Where(Function(a) a.ParticipantID = Id).Select(Function(b) b.LastName).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_ParticipantID(Id As Integer?) As ActionResult
            ViewBag.ParticipantID = db.Participants.Where(Function(a) a.ParticipantID = Id).Select(Function(b) b.IDNumber).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_ItemName(Id As Integer?) As ActionResult
            ViewBag.ItemName = db.AddonItems.Where(Function(a) a.ItemID = Id).Select(Function(b) b.Description).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_ItemChosen(Id As Integer?) As ActionResult
            ViewBag.ItemChosen = db.AddonOptions.Where(Function(a) a.OptionID = Id).Select(Function(b) b.Size).FirstOrDefault()

            Return PartialView()
        End Function

        Function Get_AddonDropDown(Id As Integer?, SaleID As Integer?) As ActionResult
            ViewBag.Addon = New SelectList(db.AddonOptions.Where(Function(a) a.ItemID = Id), "OptionID", "Size")
            ViewBag.SaleID = SaleID
            Return PartialView()
        End Function

        Function Get_SubLink(Id As Integer?) As ActionResult
            ViewBag.EntryID = Id
            Dim RaceID = db.Entries.Where(Function(a) a.EntryID = Id).Select(Function(b) b.RaceID).FirstOrDefault()
            ViewBag.Upgrade = db.Entries.Where(Function(a) a.EntryID = Id).Select(Function(b) b.DistanceChange).FirstOrDefault()
            If db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.OpenSubLinks).FirstOrDefault() < Now() Then
                ViewBag.SubOpen = True
            Else
                ViewBag.SubOpen = False
            End If
            Return PartialView()
        End Function

        Function GenerateTicket(Id As Integer?) As FileResult

            Dim url As String = "https://192.168.1.20/Entries/IssueTicket/" + Id.ToString()
            Dim converter As New HtmlToPdf()
            Dim doc As PdfDocument = converter.ConvertUrl(url)
            Dim stream As New MemoryStream()
            doc.Save(stream)
            doc.Close()
            Return File(stream.ToArray(), "application/pdf", "ProntoEntries_Ticket_" + Id.ToString() + ".pdf")

            'Dim MyPDF As New Rotativa.ActionAsPdf("IssueTicket", New With {.id = Id})
            'MyPDF.FileName = "ProntoEntries_Ticket_" + Id.ToString() + ".pdf"
            'MyPDF.PageSize = Rotativa.Options.Size.A4
            'MyPDF.PageMargins = New Rotativa.Options.Margins(20, 20, 20, 20)
            'Return MyPDF
        End Function

        ' GET: Entries
        Function IssueTicket(Id As Integer?) As ActionResult
            Dim RaceID = db.Entries.Where(Function(b) b.EntryID = Id).Select(Function(c) c.RaceID).FirstOrDefault()
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(RaceID)
            ViewBag.Background = raceEvent.Image

            Dim EntriesContent = db.Entries.Where(Function(a) a.EntryID = Id)
            Return PartialView(EntriesContent.ToList())

        End Function

        Function UpdateEntry(Id As Integer?) As ActionResult

            Return View()
        End Function

        ' GET: Entries
        <Authorize>
        Function Index(ByVal SearchValue As String) As ActionResult
            Dim EntriesContent = db.Entries.Where(Function(a) a.Status <> "UnPaid" And User.Identity.Name = a.MainUserID)
            ViewBag.SearchText = SearchValue
            If User.IsInRole("Admin") Then
                If SearchValue Is Nothing Then
                    EntriesContent = db.Entries.Where(Function(a) a.Status <> "UnPaid")
                Else
                    Dim Participant = db.Participants.Where(Function(b) b.FirstName.Contains(SearchValue) Or b.LastName.Contains(SearchValue) _
                                                        Or b.IDNumber.Contains(SearchValue) Or b.Mobile.Contains(SearchValue) Or b.EmailAddress.Contains(SearchValue)).OrderBy(Function(a) a.LastName)
                    EntriesContent = db.Entries.Where(Function(a) a.Status <> "UnPaid" And Participant.Any(Function(b) b.ParticipantID = a.ParticipantID))
                End If
            Else
                If User.IsInRole("Org") Then
                    Dim OrgID = db.CustomUserRoles.Where(Function(a) a.UserEmail = User.Identity.Name).Select(Function(b) b.OrgId).FirstOrDefault()
                    Dim Races = db.RaceEvents.Where(Function(a) a.OrgID = OrgID)
                    If SearchValue Is Nothing Then
                        EntriesContent = db.Entries.Where(Function(a) a.Status <> "UnPaid" And Races.Any(Function(b) b.RaceID = a.RaceID))
                    Else
                        Dim Participant = db.Participants.Where(Function(b) b.FirstName.Contains(SearchValue) Or b.LastName.Contains(SearchValue) _
                                                            Or b.IDNumber.Contains(SearchValue) Or b.Mobile.Contains(SearchValue) Or b.EmailAddress.Contains(SearchValue)).OrderBy(Function(a) a.LastName)
                        EntriesContent = db.Entries.Where(Function(a) a.Status <> "UnPaid" And Participant.Any(Function(b) b.ParticipantID = a.ParticipantID) And Races.Any(Function(b) b.RaceID = a.RaceID))
                    End If
                End If
            End If

            Return View(EntriesContent.ToList())
        End Function

        ' GET: Entries/Details/5
        <Authorize>
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entry As Entry = db.Entries.Find(id)
            If IsNothing(entry) Then
                Return HttpNotFound()
            End If
            Return View(entry)
        End Function

        ' GET: Entries/Create
        <Authorize>
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Entries/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Create(<Bind(Include:="EntryID,ParticipantID,RaceID,DivisionID,Amount,Status,PaymentReference,DistanceChange,ChangePaymentRef,TransferID,Result")> ByVal entry As Entry) As ActionResult
            If ModelState.IsValid Then
                db.Entries.Add(entry)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(entry)
        End Function

        Function Update(ByVal id As Integer?, ByVal OptionID As Integer?, ByVal SaleID As Integer?, ByVal NewOptionID As Integer?) As ActionResult
            Dim Sale = db.Sales.Where(Function(a) a.SaleID = SaleID).FirstOrDefault()
            ViewBag.ParticipantID = Sale.ParticipantID
            ViewBag.OptionID = New SelectList(db.AddonOptions.Where(Function(a) a.ItemID = id), "OptionID", "Size", OptionID)


            Return View(Sale)
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Update(<Bind(Include:="SaleID,RaceID,DivisionID,ItemID,OptionID,UserID,Indemnity,TandC,ParticipantID,M_reference,Pf_Reference,Verified,SaleDate")> ByVal Sale As Sale) As ActionResult
            If ModelState.IsValid Then
                db.Entry(Sale).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(Sale)
        End Function

        ' GET: Entries/Edit/5
        <Authorize>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entry As Entry = db.Entries.Find(id)
            Dim Sales = db.Sales.Where(Function(a) a.ParticipantID = entry.ParticipantID And a.M_reference = entry.PaymentReference And a.RaceID Is Nothing)

            ViewBag.ParticipantID = entry.ParticipantID


            If IsNothing(entry) Then
                Return HttpNotFound()
            End If
            Return View(Sales.ToList())
        End Function

        ' POST: Entries/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Edit(<Bind(Include:="EntryID,ParticipantID,RaceID,DivisionID,Amount,Status,PaymentReference,DistanceChange,ChangePaymentRef,TransferID,Result")> ByVal entry As Entry) As ActionResult
            If ModelState.IsValid Then
                db.Entry(entry).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(entry)
        End Function

        ' GET: Entries/Transfer/5
        <Authorize>
        Function Transfer(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entry As Entry = db.Entries.Find(id)
            If IsNothing(entry) Then
                Return HttpNotFound()
            End If
            Return View(entry)
        End Function

        ' POST: Entries/Transfer/5
        <HttpPost()>
        <ActionName("Transfer")>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Transfer(ByVal id As Integer) As ActionResult
            Return RedirectToAction("AdminToPayfast", "Entries", New With {.id = id, .Type = 1})
        End Function

        ' GET: Entries/Upgrade/5
        <Authorize>
        Function Upgrade(ByVal id As Integer?, ByVal DivisionSelect As Decimal?) As ActionResult
            Dim RaceID = db.Entries.Where(Function(a) a.EntryID = id).Select(Function(b) b.RaceID).FirstOrDefault()

            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entry As Entry = db.Entries.Find(id)
            Dim DistanceSelected = db.Divisions.Where(Function(a) a.DivisionID = entry.DivisionID).Select(Function(b) b.Distance).FirstOrDefault()
            ViewBag.CurrentDistance = DistanceSelected
            ViewBag.DivisionSelect = DivisionSelect

            Dim Distances = db.Divisions.Where(Function(a) a.RaceID = RaceID And a.Distance <> DistanceSelected).Select(Function(b) b.Distance).Distinct()
            ViewBag.Distance = New SelectList(Distances, Nothing, Nothing, DivisionSelect)

            If IsNothing(entry) Then
                Return HttpNotFound()
            End If
            Return View(entry)
        End Function

        ' POST: Entries/Upgrade/5
        <Authorize>
        Function PerformUpgrade(ByVal id As Integer, ByVal DivisionSelect As Decimal?) As ActionResult
            Dim entry As Entry = db.Entries.Find(id)
            Dim CurrentDivision = db.Divisions.Where(Function(a) a.DivisionID = entry.DivisionID).FirstOrDefault()
            Dim NewDivision = db.Divisions.Where(Function(a) a.Distance = DivisionSelect And a.Category = CurrentDivision.Category).FirstOrDefault()
            Dim PriceDifference = NewDivision.Price - CurrentDivision.Price

            If PriceDifference > 0 Then
                Return RedirectToAction("AdminUpgradeToPayfast", "Entries", New With {.id = id, .divisionID = NewDivision.DivisionID, .Price = PriceDifference, .Type = 2})
            Else
                Return RedirectToAction("AdminUpgradeToPayfast", "Entries", New With {.id = id, .divisionID = NewDivision.DivisionID, .Price = 0.00, .Type = 2})
            End If


            Return View(entry)
        End Function

        ' GET: Entries/Delete/5
        <Authorize>
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entry As Entry = db.Entries.Find(id)
            If IsNothing(entry) Then
                Return HttpNotFound()
            End If
            Return View(entry)
        End Function

        ' POST: Entries/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim entry As Entry = db.Entries.Find(id)
            db.Entries.Remove(entry)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
