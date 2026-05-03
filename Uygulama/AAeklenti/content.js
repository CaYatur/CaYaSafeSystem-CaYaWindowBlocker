// content.js içeriği
function remove_custom_element(selector) {
    var elements = document.querySelectorAll(`[jsname="${selector}"], .${selector}`);
    elements.forEach(function(element) {
        element.parentNode.removeChild(element);
        console.log(`${selector} öğeleri kaldırıldı.`);
    });
}

function remove_custom_element_classname(classNames) {
    var classes = classNames.split(/\s+/); // Boşluklara göre ayır

    classes.forEach(function(className) {
        var elements = document.querySelectorAll(`.${className}`);
        elements.forEach(function(element) {
            element.parentNode.removeChild(element);
            console.log(`${className} sınıfına sahip öğeler kaldırıldı.`);
        });
    });
}

function remove_custom_html(html) {
    var elements = document.querySelectorAll(html);
    elements.forEach(function(element) {
        element.parentNode.removeChild(element);
        console.log("Belirtilen HTML öğeleri kaldırıldı.");
    });
}

function removeVideosContent() {
    var videosContent = document.querySelector('[data-hveid="CAMQAw"]'); // Videolar içeriğini temsil eden bir öğe seçici
    if (videosContent) {
        videosContent.parentNode.removeChild(videosContent);
        console.log("Videolar içeriği kaldırıldı.");
    }
}

// Videolar sekmesine tıklandığında çalışacak olan işlev
function handleVideosTabClick() {
    var videosTab = document.querySelector('a[href*="tbm=vid"]');
    if (videosTab) {
        videosTab.addEventListener('click', function() {
            // Videolar sekmesine tıklandığında çağrılacak işlev
            removeVideosContent();
        });
    }
}


function remove_custom_elements() {
    remove_custom_element('tX7jT');
    remove_custom_element('KG3hVc');
    remove_custom_element('pKB8Bc');
    remove_custom_element('s2gQvd');
    remove_custom_element('a9kxte');
    //remove_custom_element('uU7dJb');
    //remove_custom_element('oYxtQd');
    
    // Sadece class'a sahip öğeleri sil
    //remove_custom_element_classname('SenEzd oYWfcb OSrXXb RB2q5e');

    // Belirtilen HTML öğelerini sil
    remove_custom_html('.hdtb-mitem > a[href*="tbm=vid"]');
    remove_custom_html('.uVMCKf[data-hveid="CAwQAA"]');
    remove_custom_html('.uVMCKf[data-hveid="CAwQAA"]');
    remove_custom_html('img[src^="data:image/jpg;base64"]');
    remove_custom_html('body[style="background: black; overflow: hidden; visibility: visible;"]');
    remove_custom_html('.app-frame.app-imagery-mode');

    //remove_custom_html('.EyBRub[data-hveid="CAoQAA"]');
}

// Sayfa tamamen yüklendikten sonra çalıştır
window.addEventListener('load', function() {
    // İçeriğinizi silme işlemlerini gerçekleştir
    remove_custom_elements();
    handleVideosTabClick();
    removeVideosContent();
    remove_custom_html();

    // DOM değişikliklerini bekleyerek içeriği tekrar kontrol et
    var observer = new MutationObserver(function(mutations) {
        remove_custom_elements();
        removeVideosContent();
        remove_custom_html();
    });

    observer.observe(document.documentElement, { subtree: true, childList: true });
});

console.log("Removed Videos");

