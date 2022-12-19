@If ViewBag.CartAmount = 0 Then
    @<div>
       Included
    </div>
Else
    @<div>
        R
        @ViewBag.CartAmount
    </div>
End If
