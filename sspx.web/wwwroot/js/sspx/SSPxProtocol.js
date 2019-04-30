function getIconClass(leftMenu, itemTypeLookup) {
    if (itemTypeLookup == 24) {
        return leftMenu ? 'far fa-h-square' : 'fas fa-h-square';
    }
    else if (itemTypeLookup == 12 || itemTypeLookup == 26) {
        return leftMenu ? 'fal fa-font' : 'fas fa-font';
    }
    else if (itemTypeLookup == 4) {
        return leftMenu ? 'far fa-dot-circle' : 'fas fa-dot-circle';
    }
    else if (itemTypeLookup == 23) {
        return leftMenu ? 'far fa-check-square' : 'fas fa-check-square';
    }
    else if (itemTypeLookup == 17) {
        return leftMenu ? 'far fa-question-square' : 'fas fa-question-square';
    }
    else if (itemTypeLookup == 6) {
        return leftMenu ? 'far fa-file-alt' : 'fas fa-file-alt';
    }
    else if (itemTypeLookup == 20) {
        return leftMenu ? 'fal fa-edit' : 'fas fa-pen-square';
    }
    return '';
}