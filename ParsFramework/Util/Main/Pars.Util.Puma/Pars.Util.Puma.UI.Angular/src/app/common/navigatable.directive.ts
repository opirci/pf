//import { Directive} from "@angular/core";


//@Directive({
//    selector:""
//})
//export class NavigatableDirective {



//    //restrict = 'A';
//    //scope: {
//    //    agNavigableTable: '@'
//    //},      
//    //link(scope, el) {
//    //    var firstVisibleInput = function (node, tdIndex) {
//    //        if (tdIndex !== undefined) {
//    //            node = node.children[tdIndex] //todo what if there are varying colspans?				  
//    //        }
//    //        if (node) {
//    //            var descendants = node.getElementsByTagName('*')
//    //            for (var i = 0; i < descendants.length; i++) {
//    //                if (descendants[i].nodeName === 'INPUT'
//    //                    && descendants[i].offsetHeight > 0 && descendants[i].offsetWidth > 0 //the input is actually visible
//    //                ) {
//    //                    return descendants[i]
//    //                }
//    //            }
//    //        }
//    //        return undefined
//    //    }

//    //    var disableLeftRight = false

//    //    if (scope.agNavigableTable === 'excel') {
//    //        el.bind('dblclick', function () {
//    //            disableLeftRight = true
//    //            document.activeElement.onblur = function () {
//    //                disableLeftRight = false
//    //            }
//    //        })
//    //    }


//    //    el.bind('keydown', function (event) {
//    //        var keys = { left: 37, up: 38, right: 39, down: 40 }
//    //        var key = event.which

//    //        if (scope.agNavigableTable === 'excel') {
//    //            if (key === 13) {
//    //                key = 40 //in excel mode, treat the enter key like the down arrow
//    //            }
//    //        } else {
//    //            if (key === 13) {
//    //                disableLeftRight = !disableLeftRight //in standard mode, the enter key disables the left/right arrows
//    //                document.activeElement.onblur = function () {
//    //                    disableLeftRight = false
//    //                }
//    //            }
//    //        }

//    //        //start at the currently focused element, must be an input for this to continue
//    //        var startInput = document.activeElement
//    //        if (startInput.nodeName !== 'INPUT') return;


//    //        var destinationInput
//    //        var node = startInput

//    //        //walk along the DOM, looking for the destination input 

//    //        //look for the startInput's TD
//    //        do {
//    //            node = node.parentNode
//    //        } while (node && node.nodeName !== 'TD')
//    //        if (!node) return //ill-formed html

//    //        if (!disableLeftRight && (key === keys.left || key === keys.right)) {
//    //            event.preventDefault()
//    //            //walk along the TDs until we find one that has a visible input within it				  
//    //            do {
//    //                node = (key === keys.left ? node.previousElementSibling : node.nextElementSibling)
//    //                if (node && node.nodeName === 'TD') {
//    //                    destinationInput = firstVisibleInput(node)
//    //                }
//    //                else return //no more TDs available - we're done here
//    //            } while (!destinationInput)
//    //        }
//    //        else if (key === keys.up || key === keys.down) {
//    //            event.preventDefault()
//    //            var tdIndex = node.cellIndex;

//    //            do { //find the TR
//    //                node = node.parentNode
//    //            } while (node && node.nodeName !== 'TR')
//    //            if (!node) return //ill-formed html

//    //            do {
//    //                node = (key === keys.up ? node.previousElementSibling : node.nextElementSibling)
//    //                if (node && node.nodeName === 'TR') {
//    //                    destinationInput = firstVisibleInput(node, tdIndex)
//    //                }
//    //                else return //no more rows or ill-formed html
//    //            } while (!destinationInput)

//    //        }

//    //        if (destinationInput) {
//    //            destinationInput.focus()
//    //        }


//    //    })


//    //return function (scope, element, attr) {
//    //element.on('keypress.mynavigation', 'input[type="text"]', handleNavigation);
//    //function handleNavigation(e) {

//    //    var arrow = { left: 37, up: 38, right: 39, down: 40 };

//    //    // select all on focus
//    //    element.find('input').keydown(function (e) {

//    //        // shortcut for key other than arrow keys
//    //        if ($.inArray(e.which, [arrow.left, arrow.up, arrow.right, arrow.down]) < 0) {
//    //            return;
//    //        }

//    //        var input = e.target;
//    //        var td = $(e.target).closest('td');
//    //        var moveTo = null;

//    //        switch (e.which) {

//    //            case arrow.left:
//    //                {
//    //                    if (input.selectionStart == 0) {
//    //                        moveTo = td.prev('td:has(input,textarea)');
//    //                    }
//    //                    break;
//    //                }
//    //            case arrow.right:
//    //                {
//    //                    if (input.selectionEnd == input.value.length) {
//    //                        moveTo = td.next('td:has(input,textarea)');
//    //                    }
//    //                    break;
//    //                }

//    //            case arrow.up:
//    //            case arrow.down:
//    //                {

//    //                    var tr = td.closest('tr');
//    //                    var pos = td[0].cellIndex;

//    //                    var moveToRow = null;
//    //                    if (e.which == arrow.down) {
//    //                        moveToRow = tr.next('tr');
//    //                    }
//    //                    else if (e.which == arrow.up) {
//    //                        moveToRow = tr.prev('tr');
//    //                    }

//    //                    if (moveToRow.length) {
//    //                        moveTo = $(moveToRow[0].cells[pos]);
//    //                    }

//    //                    break;
//    //                }

//    //        }

//    //        if (moveTo && moveTo.length) {

//    //            e.preventDefault();

//    //            moveTo.find('input,textarea').each(function (i, input) {
//    //                input.focus();
//    //                input.select();
//    //            });

//    //        }

//    //    });


//    //    var key = e.keyCode ? e.keyCode : e.which;
//    //    if (key === 13) {
//    //        var focusedElement = $(e.target);
//    //        var nextElement = focusedElement.parent().next();
//    //        if (nextElement.find('input').length > 0) {
//    //            nextElement.find('input').focus();
//    //        } else {
//    //            nextElement = nextElement.parent().next().find('input').first();
//    //            nextElement.focus();
//    //        }
//    //    }
//    //}
//};
//}