@Imports Microsoft.AspNet.Identity

@If Request.IsAuthenticated
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm", .class = "navbar-right"})
        @Html.AntiForgeryToken()
        @<ul class="nav navbar-nav navbar-right">
             @*<li>
                 @Html.ActionLink("Account", "Index", "Manage", routeValues:=Nothing, htmlAttributes:=New With {.title = "Manage"})
                 "Hello " + User.Identity.GetUserName() + "!"
             </li>*@
            <li><a href="javascript:document.getElementById('logoutForm').submit()">Log off</a></li>
        </ul>
    End Using
Else
    @<ul class="nav navbar-nav">
        <li>@Html.ActionLink("Register", "Register", "Account", New With {.returnurl = "~/RaceEvents/Index"}, htmlAttributes:=New With {.id = "registerLink"})</li>
        <li>@Html.ActionLink("Log in", "Login", "Account", New With {.returnurl = "~/RaceEvents/Index"}, htmlAttributes:=New With {.id = "loginLink"})</li>
    </ul>
End If

