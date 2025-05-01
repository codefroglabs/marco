export function initMarco() {
   // initResizables();
}

function enableResize(resizable){
    const handles = resizable.querySelectorAll('.resize-handle');

    handles.forEach(handle => {
        handle.addEventListener('mousedown', (e) => {
            e.preventDefault();

            const isTop = handle.classList.contains('top');
            const isRight = handle.classList.contains('right');
            const isBottom = handle.classList.contains('bottom');
            const isLeft = handle.classList.contains('left');

            const startX = e.clientX;
            const startY = e.clientY;
            const startWidth = parseInt(document.defaultView.getComputedStyle(resizable).width, 10);
            const startHeight = parseInt(document.defaultView.getComputedStyle(resizable).height, 10);

            function doDrag(event) {
                if (isRight) {
                    resizable.style.width = startWidth + event.clientX - startX + 'px';
                } else if (isLeft) {
                    resizable.style.width = startWidth - (event.clientX - startX) + 'px';
                }
                if (isBottom) {
                    resizable.style.height = startHeight + event.clientY - startY + 'px';
                } else if (isTop) {
                    resizable.style.height = startHeight - (event.clientY - startY) + 'px';
                }
            }

            function stopDrag() {
                document.removeEventListener('mousemove', doDrag);
                document.removeEventListener('mouseup', stopDrag);
            }

            document.addEventListener('mousemove', doDrag);
            document.addEventListener('mouseup', stopDrag);
        });
    });
}

function initResizables() {
    document.querySelectorAll('.resizable').forEach(resizable => {
        enableResize(resizable);
    });
}
