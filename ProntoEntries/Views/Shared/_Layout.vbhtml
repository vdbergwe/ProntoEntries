@Imports Microsoft.AspNet.Identity

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <title>@ViewBag.Title - Pronto Entries</title>
    <link href="https://fonts.googleapis.com/css?family=Raleway:700%2C600" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/8466fbcbdc.js" crossorigin="anonymous"></script>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    <link href="~/Content/bootstrap-chosen.css" rel="stylesheet" />
    <link href="~/Content/MainLayout.css" rel="stylesheet" />
</head>
<body class="mainbody">
    <div class="backing"></div>
    <div class="containertop">
        <div class="logobox">
            @*<img src="~/Content/Pronto-Logo-Black.png" alt="Pronto Computer Solutions Logo" class="logoimage" />*@
        </div>
        <div class="navright">
            <input type="checkbox" class="openSidebarMenu" id="openSidebarMenu" />
            <label for="openSidebarMenu" class="sidebarIconToggle">
                <div class="spinner diagonal part-1"></div>
                <div class="spinner horizontal"></div>
                <div class="spinner diagonal part-2"></div>
            </label>
            <div class="menuarea" id="sidebarMenu">
                <ul class="sidebarMenuInner">
                    @If User.Identity.IsAuthenticated And (User.IsInRole("Admin") Or User.IsInRole("SuperUser")) Then
                        @<li>@Html.ActionLink("Organiser", "Index", "Organisers")</li>
                    End If
                    <li>@Html.ActionLink("Events", "Index", "RaceEvents")</li>
                    @If User.Identity.IsAuthenticated And (User.IsInRole("Admin") Or User.IsInRole("SuperUser")) Then
                        @<li>@Html.ActionLink("Addons", "Index", "AddonItems")</li>
                    End If
                    @If User.Identity.IsAuthenticated And Not ((User.IsInRole("Admin") Or User.IsInRole("Org") Or User.IsInRole("SuperUser"))) Then
                        @<li>@Html.ActionLink("Entries", "Index", "Entries")</li>
                    End If
                    @If User.Identity.IsAuthenticated And Not ((User.IsInRole("Admin") Or User.IsInRole("Org") Or User.IsInRole("SuperUser"))) Then
                        @<li>@Html.ActionLink("Participants", "Index", "Participants")</li>
                    End If
                    @If User.Identity.IsAuthenticated And Not ((User.IsInRole("Admin") Or User.IsInRole("Org") Or User.IsInRole("SuperUser"))) Then
                        @<li>@Html.ActionLink("Results", "Index", "RaceEvents")</li>
                    End If
                    @If User.Identity.IsAuthenticated And (User.IsInRole("Admin") Or User.IsInRole("Org") Or User.IsInRole("SuperUser")) Then
                        @<li>@Html.ActionLink("Search", "Index", "Reports")</li>
                    End If
                    @If User.Identity.IsAuthenticated And (User.IsInRole("Admin") Or User.IsInRole("Org") Or User.IsInRole("SuperUser")) Then
                        @<li>@Html.ActionLink("Dashboard", "Dashboard", "Reports")</li>
                    End If
                    @If User.Identity.IsAuthenticated And Not ((User.IsInRole("Admin") Or User.IsInRole("Org") Or User.IsInRole("SuperUser"))) Then
                        @<li>@Html.ActionLink("Cart", "Cart", "Entries")</li>
                    End If

                    @If Request.IsAuthenticated Then

                        @*<li>
                                @Html.ActionLink("Account", "Index", "Manage", routeValues:=Nothing, htmlAttributes:=New With {.title = "Manage"})
                                "Hello " + User.Identity.GetUserName() + "!"
                            </li>*@
                        @<li>
                            @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm", .class = "sidebarMenuInnerMobile"})
                                @Html.AntiForgeryToken()

                                @<a href="javascript:document.getElementById('logoutForm').submit()"> Log off</a>
                            End Using
                        </li>

                    Else

                        @<li>@Html.ActionLink("Register", "Register", "Account", New With {.returnurl = "~/RaceEvents/Index"}, htmlAttributes:=New With {.id = "registerLink"})</li>
                        @<li>@Html.ActionLink("Log in", "Login", "Account", New With {.returnurl = "~/RaceEvents/Index"}, htmlAttributes:=New With {.id = "loginLink"})</li>

                    End If
                </ul>

            </div>
            @If Request.IsAuthenticated Then

                @<ul style="padding-right: 20px;">

                    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm", .class = "sidebarMenuInnerDesktop"})
                        @Html.AntiForgeryToken()
                        @<li>
                            <a href="javascript:document.getElementById('logoutForm').submit()"> Log off</a>
                        </li>
                    End Using
                </ul>

            End If
            @*<div class="navitems" style="padding-right: 20px;">
                    @Html.Partial("_LoginPartial")
                </div>*@
        </div>


        @* Mobile Menu *@


    </div>
    <div class="container body-content">
        @RenderBody()
        @*<hr />*@
        <footer>
            <p>&copy;@DateTime.Now.Year - Pronto Computer Solutions PTY (Ltd) </p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @*@Scripts.Render("~/bundles/bootstrap")*@
    <script>
        $("#containertop").addClass('color-whitetrans')


        // trigger this function every time the user scrolls
        window.onscroll = function (event) {
            var scroll = window.pageYOffset;
            if (scroll < 100) {
                $(".containertop").addClass('color-whitetrans');
                $(".containertop").removeClass('color-white');
            } else if (scroll >= 100) {
                $(".containertop").addClass('color-white');
                $(".containertop").removeClass('color-whitetrans');

            }
        }
    </script>

    @RenderSection("scripts", required:=False)
    <script src="~/Scripts/chosen.jquery.js"></script>
</body>
</html>
