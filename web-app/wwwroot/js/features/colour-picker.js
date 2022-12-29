import * as presentationApi from "../api/presentation.js";
import { debounce } from "../utilities.js";


const selectors = {
    activator: '#colour-picker-activator',
    close: '#colour-picker-close',
    component: '.c-colour-picker',
    overlay: '.slide-frame > .o-overlay',
    scriptId: 'colour-picker-script'
}
    
const { presentationId, url } = retreiveParams();

$(selectors.activator).click(show);
$(selectors.close).click(close);

$.farbtastic('#colorpicker', debounce((color) => {

    $(selectors.overlay).css('background', color); //TODO: Extract this line on ajax call success. Check for json token error.
    presentationApi.changeBackground(url, { presentationId, background: color });
}));

function show() {
    $(selectors.component).show();
}

function close() {
    $(selectors.component).hide();
}

function retreiveParams() {
    const element = document.getElementById(selectors.scriptId);

    const presentationId = element.getAttribute('data-presentationId');
    const background = element.getAttribute('data-background');
    const url = element.getAttribute('data-url');

    return {
        presentationId,
        background,
        url
    }
}