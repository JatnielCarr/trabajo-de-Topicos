// Configuración de la API
const API_BASE_URL = 'https://localhost:7001/api';

// Estado global de la aplicación
let currentUser = null;
let currentSection = 'home';

// Elementos del DOM
const moviesGrid = document.getElementById('moviesGrid');
const sectionTitle = document.getElementById('sectionTitle');
const searchInput = document.getElementById('searchInput');
const searchBtn = document.getElementById('searchBtn');
const loading = document.getElementById('loading');
const movieModal = document.getElementById('movieModal');
const loginModal = document.getElementById('loginModal');
const registerModal = document.getElementById('registerModal');

// Inicialización
document.addEventListener('DOMContentLoaded', function() {
    initializeApp();
    setupEventListeners();
    loadMovies();
    checkAuthStatus();
});

function initializeApp() {
    // Configurar fecha por defecto para el registro
    const dateInput = document.getElementById('registerDateOfBirth');
    if (dateInput) {
        const today = new Date();
        const maxDate = new Date(today.getFullYear() - 13, today.getMonth(), today.getDate());
        dateInput.max = maxDate.toISOString().split('T')[0];
    }
}

function setupEventListeners() {
    // Navegación
    document.querySelectorAll('.nav-link').forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const section = e.target.dataset.section;
            navigateToSection(section);
        });
    });

    // Búsqueda
    searchBtn.addEventListener('click', performSearch);
    searchInput.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') {
            performSearch();
        }
    });

    // Modales
    setupModalListeners();

    // Formularios de autenticación
    setupAuthForms();

    // Botones de autenticación
    document.getElementById('loginBtn').addEventListener('click', () => showModal(loginModal));
    document.getElementById('registerBtn').addEventListener('click', () => showModal(registerModal));
    document.getElementById('logoutBtn').addEventListener('click', logout);
}

function setupModalListeners() {
    // Cerrar modales
    document.querySelectorAll('.close').forEach(closeBtn => {
        closeBtn.addEventListener('click', () => {
            hideAllModals();
        });
    });

    // Cerrar modales al hacer clic fuera
    window.addEventListener('click', (e) => {
        if (e.target.classList.contains('modal')) {
            hideAllModals();
        }
    });
}

function setupAuthForms() {
    // Formulario de login
    document.getElementById('loginForm').addEventListener('submit', async (e) => {
        e.preventDefault();
        await handleLogin();
    });

    // Formulario de registro
    document.getElementById('registerForm').addEventListener('submit', async (e) => {
        e.preventDefault();
        await handleRegister();
    });
}

// Navegación
function navigateToSection(section) {
    currentSection = section;
    
    // Actualizar navegación activa
    document.querySelectorAll('.nav-link').forEach(link => {
        link.classList.remove('active');
    });
    document.querySelector(`[data-section="${section}"]`).classList.add('active');

    // Cargar contenido según la sección
    switch (section) {
        case 'home':
            sectionTitle.textContent = 'Todas las Películas';
            loadMovies();
            break;
        case 'featured':
            sectionTitle.textContent = 'Películas Destacadas';
            loadFeaturedMovies();
            break;
        case 'new':
            sectionTitle.textContent = 'Películas Nuevas';
            loadNewMovies();
            break;
        case 'favorites':
            if (!currentUser) {
                showModal(loginModal);
                return;
            }
            sectionTitle.textContent = 'Mis Favoritos';
            loadFavorites();
            break;
        case 'history':
            if (!currentUser) {
                showModal(loginModal);
                return;
            }
            sectionTitle.textContent = 'Historial de Visualización';
            loadWatchHistory();
            break;
    }
}

// Carga de películas
async function loadMovies() {
    showLoading();
    try {
        const response = await fetch(`${API_BASE_URL}/movies`);
        const movies = await response.json();
        displayMovies(movies);
    } catch (error) {
        console.error('Error cargando películas:', error);
        showError('Error al cargar las películas');
    } finally {
        hideLoading();
    }
}

async function loadFeaturedMovies() {
    showLoading();
    try {
        const response = await fetch(`${API_BASE_URL}/movies/featured`);
        const movies = await response.json();
        displayMovies(movies);
    } catch (error) {
        console.error('Error cargando películas destacadas:', error);
        showError('Error al cargar las películas destacadas');
    } finally {
        hideLoading();
    }
}

async function loadNewMovies() {
    showLoading();
    try {
        const response = await fetch(`${API_BASE_URL}/movies/new`);
        const movies = await response.json();
        displayMovies(movies);
    } catch (error) {
        console.error('Error cargando películas nuevas:', error);
        showError('Error al cargar las películas nuevas');
    } finally {
        hideLoading();
    }
}

async function loadFavorites() {
    if (!currentUser) return;
    
    showLoading();
    try {
        const response = await fetch(`${API_BASE_URL}/movies/favorites`, {
            headers: {
                'Authorization': `Bearer ${currentUser.token}`
            }
        });
        const movies = await response.json();
        displayMovies(movies);
    } catch (error) {
        console.error('Error cargando favoritos:', error);
        showError('Error al cargar los favoritos');
    } finally {
        hideLoading();
    }
}

async function loadWatchHistory() {
    if (!currentUser) return;
    
    showLoading();
    try {
        const response = await fetch(`${API_BASE_URL}/movies/history`, {
            headers: {
                'Authorization': `Bearer ${currentUser.token}`
            }
        });
        const movies = await response.json();
        displayMovies(movies);
    } catch (error) {
        console.error('Error cargando historial:', error);
        showError('Error al cargar el historial');
    } finally {
        hideLoading();
    }
}

// Búsqueda
async function performSearch() {
    const query = searchInput.value.trim();
    if (!query) return;

    showLoading();
    try {
        const response = await fetch(`${API_BASE_URL}/movies/search?q=${encodeURIComponent(query)}`);
        const movies = await response.json();
        sectionTitle.textContent = `Resultados de búsqueda: "${query}"`;
        displayMovies(movies);
    } catch (error) {
        console.error('Error en la búsqueda:', error);
        showError('Error al realizar la búsqueda');
    } finally {
        hideLoading();
    }
}

// Visualización de películas
function displayMovies(movies) {
    if (!movies || movies.length === 0) {
        moviesGrid.innerHTML = '<p style="text-align: center; color: rgba(255,255,255,0.7); grid-column: 1/-1;">No se encontraron películas</p>';
        return;
    }

    moviesGrid.innerHTML = movies.map(movie => `
        <div class="movie-card" data-movie-id="${movie.id}">
            <img src="${movie.posterUrl}" alt="${movie.title}" class="movie-poster" onerror="this.src='https://via.placeholder.com/300x450/1a1f2e/ffffff?text=Sin+Imagen'">
            <div class="movie-info">
                <h3 class="movie-title">${movie.title}</h3>
                <div class="movie-meta">
                    <span class="movie-rating">
                        <i class="fas fa-star"></i>
                        ${movie.rating}
                    </span>
                    <span class="movie-genre">${movie.genre}</span>
                </div>
                <div class="movie-meta">
                    <span>${movie.year}</span>
                    <span>${formatDuration(movie.duration)}</span>
                </div>
                <div class="movie-actions">
                    <button class="favorite-btn ${movie.isFavorite ? 'active' : ''}" onclick="toggleFavorite(${movie.id}, this)">
                        <i class="fas fa-heart"></i>
                    </button>
                    <button class="watch-btn" onclick="showMovieDetails(${movie.id})">
                        <i class="fas fa-play"></i> Ver
                    </button>
                </div>
            </div>
        </div>
    `).join('');
}

// Detalles de película
async function showMovieDetails(movieId) {
    showLoading();
    try {
        const response = await fetch(`${API_BASE_URL}/movies/${movieId}`);
        const movie = await response.json();
        
        const movieDetails = document.getElementById('movieDetails');
        movieDetails.innerHTML = `
            <img src="${movie.posterUrl}" alt="${movie.title}" class="movie-details-poster" onerror="this.src='https://via.placeholder.com/300x450/1a1f2e/ffffff?text=Sin+Imagen'">
            <div class="movie-details-info">
                <h2>${movie.title}</h2>
                <div class="movie-details-meta">
                    <span><i class="fas fa-star"></i> ${movie.rating}</span>
                    <span>${movie.year}</span>
                    <span>${formatDuration(movie.duration)}</span>
                    <span>${movie.genre}</span>
                </div>
                <p class="movie-details-description">${movie.description}</p>
                <div class="movie-details-cast">
                    <strong>Director:</strong> ${movie.director}<br>
                    <strong>Reparto:</strong> ${movie.cast}
                </div>
                <div class="movie-details-actions">
                    <button class="btn btn-primary" onclick="addToWatchHistory(${movie.id})">
                        <i class="fas fa-play"></i> Reproducir
                    </button>
                    <button class="btn btn-secondary" onclick="toggleFavorite(${movie.id})">
                        <i class="fas fa-heart"></i> ${movie.isFavorite ? 'Quitar de Favoritos' : 'Agregar a Favoritos'}
                    </button>
                </div>
            </div>
        `;
        
        showModal(movieModal);
    } catch (error) {
        console.error('Error cargando detalles de la película:', error);
        showError('Error al cargar los detalles de la película');
    } finally {
        hideLoading();
    }
}

// Favoritos
async function toggleFavorite(movieId, button = null) {
    if (!currentUser) {
        showModal(loginModal);
        return;
    }

    try {
        const isFavorite = button ? button.classList.contains('active') : false;
        const url = `${API_BASE_URL}/movies/${movieId}/favorites`;
        const method = isFavorite ? 'DELETE' : 'POST';
        
        const response = await fetch(url, {
            method: method,
            headers: {
                'Authorization': `Bearer ${currentUser.token}`
            }
        });

        if (response.ok) {
            if (button) {
                button.classList.toggle('active');
            }
            // Actualizar la vista si estamos en la sección de favoritos
            if (currentSection === 'favorites') {
                loadFavorites();
            }
        }
    } catch (error) {
        console.error('Error al actualizar favoritos:', error);
        showError('Error al actualizar favoritos');
    }
}

// Historial de visualización
async function addToWatchHistory(movieId) {
    if (!currentUser) {
        showModal(loginModal);
        return;
    }

    try {
        await fetch(`${API_BASE_URL}/movies/${movieId}/watch`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${currentUser.token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(0) // Duración de visualización
        });
        
        hideAllModals();
        showSuccess('Agregado al historial de visualización');
    } catch (error) {
        console.error('Error al agregar al historial:', error);
        showError('Error al agregar al historial');
    }
}

// Autenticación
async function handleLogin() {
    const username = document.getElementById('loginUsername').value;
    const password = document.getElementById('loginPassword').value;

    try {
        const response = await fetch(`${API_BASE_URL}/auth/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username, password })
        });

        if (response.ok) {
            const data = await response.json();
            currentUser = data;
            localStorage.setItem('user', JSON.stringify(data));
            updateAuthUI();
            hideAllModals();
            showSuccess('Inicio de sesión exitoso');
        } else {
            const error = await response.json();
            showError(error.message || 'Credenciales inválidas');
        }
    } catch (error) {
        console.error('Error en el login:', error);
        showError('Error al iniciar sesión');
    }
}

async function handleRegister() {
    const formData = {
        username: document.getElementById('registerUsername').value,
        email: document.getElementById('registerEmail').value,
        password: document.getElementById('registerPassword').value,
        firstName: document.getElementById('registerFirstName').value,
        lastName: document.getElementById('registerLastName').value,
        dateOfBirth: document.getElementById('registerDateOfBirth').value
    };

    try {
        const response = await fetch(`${API_BASE_URL}/auth/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });

        if (response.ok) {
            const data = await response.json();
            currentUser = data;
            localStorage.setItem('user', JSON.stringify(data));
            updateAuthUI();
            hideAllModals();
            showSuccess('Registro exitoso');
        } else {
            const error = await response.json();
            showError(error.message || 'Error en el registro');
        }
    } catch (error) {
        console.error('Error en el registro:', error);
        showError('Error al registrarse');
    }
}

function logout() {
    currentUser = null;
    localStorage.removeItem('user');
    updateAuthUI();
    showSuccess('Sesión cerrada');
    
    // Redirigir a la página principal si estamos en una sección que requiere autenticación
    if (currentSection === 'favorites' || currentSection === 'history') {
        navigateToSection('home');
    }
}

function checkAuthStatus() {
    const savedUser = localStorage.getItem('user');
    if (savedUser) {
        currentUser = JSON.parse(savedUser);
        updateAuthUI();
    }
}

function updateAuthUI() {
    const loginSection = document.getElementById('loginSection');
    const userSection = document.getElementById('userSection');
    const userName = document.getElementById('userName');

    if (currentUser) {
        loginSection.style.display = 'none';
        userSection.style.display = 'flex';
        userName.textContent = currentUser.user.firstName || currentUser.user.username;
    } else {
        loginSection.style.display = 'flex';
        userSection.style.display = 'none';
    }
}

// Utilidades
function showLoading() {
    loading.style.display = 'flex';
}

function hideLoading() {
    loading.style.display = 'none';
}

function showModal(modal) {
    hideAllModals();
    modal.style.display = 'block';
}

function hideAllModals() {
    document.querySelectorAll('.modal').forEach(modal => {
        modal.style.display = 'none';
    });
}

function formatDuration(minutes) {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return hours > 0 ? `${hours}h ${mins}m` : `${mins}m`;
}

function showSuccess(message) {
    // Implementar notificación de éxito
    alert(message);
}

function showError(message) {
    // Implementar notificación de error
    alert('Error: ' + message);
} 