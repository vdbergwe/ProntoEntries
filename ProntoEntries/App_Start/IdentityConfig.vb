Imports System.Threading.Tasks
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Configuration

Public Class EmailService
    Implements IIdentityMessageService

    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        ' Plug in your email service here to send an email.
        'Return Task.FromResult(0)
        'Return sendMail(message)
        SendMail(message)
        Return Task.FromResult(0)
    End Function

    Private Function SendMail(Message As IdentityMessage)
        Dim text As String = String.Format("Please click on this link to {0}: {1}", Message.Subject, Message.Body)
        Dim Html As String = "Please confirm your account by clicking <a href=""" & Message.Body & """>Confirmation Link</a><br/><br/>"

        Html += HttpUtility.HtmlEncode("Or copy the following link to your browser:" + Message.Body)


        Dim msg As New MailMessage With {
            .From = New MailAddress(ConfigurationManager.AppSettings("Email").ToString())
        }
        msg.To.Add(New MailAddress(Message.Destination))
        msg.Subject = Message.Subject
        msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, Nothing, MediaTypeNames.Text.Plain))
        msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(Html, Nothing, MediaTypeNames.Text.Html))

        Dim smtpClient As New SmtpClient("prontocs.co.za", Convert.ToInt32(587))
        Dim credentials As New System.Net.NetworkCredential(ConfigurationManager.AppSettings("Email").ToString(), ConfigurationManager.AppSettings("Password").ToString())
        smtpClient.Credentials = credentials
        smtpClient.EnableSsl = True
        smtpClient.Send(msg)
        Return True

    End Function
End Class

Public Class SmsService
    Implements IIdentityMessageService

    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        ' Plug in your SMS service here to send a text message.
        Return Task.FromResult(0)
    End Function
End Class

' Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
Public Class ApplicationUserManager
    Inherits UserManager(Of ApplicationUser)

    Public Sub New(store As IUserStore(Of ApplicationUser))
        MyBase.New(store)
    End Sub

    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext) As ApplicationUserManager
        Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.Get(Of ApplicationDbContext)()))

        ' Configure validation logic for usernames
        manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
            .AllowOnlyAlphanumericUserNames = False,
            .RequireUniqueEmail = True
        }

        ' Configure validation logic for passwords
        manager.PasswordValidator = New PasswordValidator With {
            .RequiredLength = 2,
            .RequireNonLetterOrDigit = False,
            .RequireDigit = True,
            .RequireLowercase = True,
            .RequireUppercase = False
        }

        ' Configure user lockout defaults
        manager.UserLockoutEnabledByDefault = True
        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
        manager.MaxFailedAccessAttemptsBeforeLockout = 5

        ' Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        ' You can write your own provider and plug it in here.
        manager.RegisterTwoFactorProvider("Phone Code", New PhoneNumberTokenProvider(Of ApplicationUser) With {
                                          .MessageFormat = "Your security code is {0}"
                                      })
        manager.RegisterTwoFactorProvider("Email Code", New EmailTokenProvider(Of ApplicationUser) With {
                                          .Subject = "Security Code",
                                          .BodyFormat = "Your security code is {0}"
                                          })
        manager.EmailService = New EmailService()
        manager.SmsService = New SmsService()
        Dim dataProtectionProvider = options.DataProtectionProvider
        If (dataProtectionProvider IsNot Nothing) Then
            manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
        End If

        Return manager
    End Function

End Class

' Configure the application sign-in manager which is used in this application.
Public Class ApplicationSignInManager
    Inherits SignInManager(Of ApplicationUser, String)
    Public Sub New(userManager As ApplicationUserManager, authenticationManager As IAuthenticationManager)
        MyBase.New(userManager, authenticationManager)
    End Sub

    Public Overrides Function CreateUserIdentityAsync(user As ApplicationUser) As Task(Of ClaimsIdentity)
        Return user.GenerateUserIdentityAsync(DirectCast(UserManager, ApplicationUserManager))
    End Function

    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationSignInManager), context As IOwinContext) As ApplicationSignInManager
        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
    End Function
End Class
