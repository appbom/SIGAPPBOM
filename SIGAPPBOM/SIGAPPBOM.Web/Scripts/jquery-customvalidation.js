$.validator.addMethod("ValidaItemsTable", function (value, element, params) {
    if (value > 0) 
        return true;
    else 
        return false; 
});
$.validator.unobtrusive.adapters.addBool("requiereitems", "ValidaItemsTable");
