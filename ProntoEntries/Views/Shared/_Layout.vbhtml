﻿<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Pronto Entries</title>
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
            <input type="checkbox" class="openSidebarMenu" id="openSidebarMenu" />
            <label for="openSidebarMenu" class="sidebarIconToggle">
                <div class="spinner diagonal part-1"></div>
                <div class="spinner horizontal"></div>
                <div class="spinner diagonal part-2"></div>
            </label>
            <div class="menuarea">
                <ul class="navitems">
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
                    @If User.Identity.IsAuthenticated And Not ((User.IsInRole("Admin") Or User.IsInRole("Org") Or User.IsInRole("SuperUser"))) Then
                        @<li>@Html.ActionLink("Cart", "Cart", "Entries")</li>
                    End If
                </ul>
            </div>
            <div class="loginarea" style="padding-right: 20px;">
                @Html.Partial("_LoginPartial")
            </div>
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
    @RenderSection("scripts", required:=False)
</body>
</html>
