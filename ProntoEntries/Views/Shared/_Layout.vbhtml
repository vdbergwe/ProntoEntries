<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - My ASP.NET Application</title>
    <link href="~/Content/MainLayout.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Raleway:700%2C600" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/8466fbcbdc.js" crossorigin="anonymous"></script>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

</head>
<body class="mainbody">
    <div class="backing"></div>
    <div class="containertop">
        <div class="logobox">
            @*<img src="~/Content/Pronto-Logo-Black.png" alt="Pronto Computer Solutions Logo" class="logoimage" />*@
        </div>
        <div class="navright">
            <div class="menuarea">
                <ul class="navitems">
                    <li>@Html.ActionLink("Organiser", "Index", "Organisers")</li>
                    <li>@Html.ActionLink("Events", "Index", "RaceEvents")</li>
                    <li>@Html.ActionLink("Entries", "Index", "Entries")</li>
                    <li>@Html.ActionLink("Participants", "Index", "Participants")</li>
                    <li>@Html.ActionLink("Results", "RaceEvents", "RaceEvents")</li>
                    <li>@Html.ActionLink("Search", "Index", "RaceEvents")</li>
                    <li>@Html.ActionLink("Cart", "Cart", "Entries")</li>
                </ul>
            </div>
            <div class="loginarea">
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>Pronto Computer Solutions PTY (Ltd) &copy;@DateTime.Now.Year</p>
        </footer>
    </div>

    @*<div class="menubar">
            <div class="logoimage">
                <img class="logotopleft" src="~/Content/Pronto-Logo-Black.png" alt="Pronto Computer Solutions Logo" />
            </div>
            <div class="menu">
                <ul class="menuitems">
                    <li>@Html.ActionLink("Home", "Index", "Home")</li>
                    <li>@Html.ActionLink("About", "About", "Home")</li>
                    @If (User.Identity.IsAuthenticated) Then
                        If (User.IsInRole("Admin")) Then
                            @<li>@Html.ActionLink("Contact", "Contact", "Home")</li>
                        End If
                    End If

                </ul>
                @Html.Partial("_LoginPartial")
            </div>
        </div>
        <div class="bottomcontainer">
            <div class="container body-content">
                @RenderBody()
                <hr />
                <footer>
                    <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
                </footer>
            </div>
        </div>*@


    @Scripts.Render("~/bundles/jquery")
    @*@Scripts.Render("~/bundles/bootstrap")*@
    @RenderSection("scripts", required:=False)
</body>
</html>
