// Archivo JavaScript para animaciones y efectos
document.addEventListener("DOMContentLoaded", () => {
    // Inicializar tooltips de Bootstrap
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map((tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl))

    // Animación para las tablas
    animateTables()

    // Efecto de hover para botones
    setupButtonHoverEffects()
})

// Función para animar las tablas
function animateTables() {
    const tables = document.querySelectorAll(".table")
    tables.forEach((table) => {
        // Animar las filas de la tabla
        const rows = table.querySelectorAll("tbody tr")
        rows.forEach((row, index) => {
            row.style.opacity = "0"
            row.style.transform = "translateX(-10px)"
            row.style.transition = "opacity 0.3s ease, transform 0.3s ease"

            setTimeout(
                () => {
                    row.style.opacity = "1"
                    row.style.transform = "translateX(0)"
                },
                100 + index * 50,
            ) // Escalonar la animación
        })
    })
}

// Función para configurar efectos de hover en botones
function setupButtonHoverEffects() {
    const buttons = document.querySelectorAll(".btn")
    buttons.forEach((button) => {
        button.addEventListener("mouseenter", function () {
            this.style.transition = "transform 0.2s ease"
            this.style.transform = "scale(1.05)"
        })

        button.addEventListener("mouseleave", function () {
            this.style.transform = "scale(1)"
        })
    })
}

// Función para mostrar un spinner de carga
function showSpinner(elementId) {
    const element = document.getElementById(elementId)
    if (element) {
        element.innerHTML =
            '<div class="spinner-border text-primary" role="status"><span class="visually-hidden">Cargando...</span></div>'
        element.style.display = "block"
    }
}

// Función para ocultar un spinner de carga
function hideSpinner(elementId) {
    const element = document.getElementById(elementId)
    if (element) {
        element.style.display = "none"
    }
}

// Función para confirmar acciones
function confirmAction(message, callback) {
    if (confirm(message)) {
        callback()
    }
}

