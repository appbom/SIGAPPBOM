/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery.validate-vsdoc.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

jQuery.validator.addMethod("requiredItems", function (value, element, param) {
    if (value > 0)
        return true;
    else
        return false;
});

jQuery.validator.unobtrusive.adapters.addSingleVal("requiredItems", "value");

