frontend/
│
├── 📄 package.json                 # Project dependencies & scripts
├── 📄 tsconfig.json                # TypeScript configuration
├── 📄 vite.config.ts               # Vite build configuration
├── 📄 tailwind.config.js           # Tailwind CSS configuration
├── 📄 postcss.config.js            # PostCSS configuration
├── 📄 index.html                   # HTML entry point
├── 📄 .gitignore                   # Git ignore rules
├── 📄 .env.example                 # Environment variables template
├── 📄 README.md                    # Project readme
├── 📄 SETUP_GUIDE.md               # Comprehensive setup guide
│
└── 📁 src/
    │
    ├── 📄 main.tsx                 # React app entry point
    ├── 📄 App.tsx                  # Main app component with routing
    ├── 📄 index.css                # Global styles & animations
    │
    ├── 📁 components/              # Reusable UI components
    │   ├── 📄 Navbar.tsx           # Top navigation bar
    │   ├── 📄 Footer.tsx           # Footer component
    │   └── 📄 ProtectedRoute.tsx   # Route protection wrapper
    │
    ├── 📁 context/                 # State management
    │   └── 📄 AuthContext.tsx      # Authentication state & hooks
    │
    ├── 📁 pages/                   # Page components
    │   ├── 📄 HomePage.tsx         # Landing page (/)
    │   ├── 📄 LoginPage.tsx        # Login form (/login)
    │   ├── 📄 SignupPage.tsx       # Influencer signup (/signup)
    │   ├── 📄 CampaignsPage.tsx    # Campaign discovery (/campaigns)
    │   ├── 📄 InfluencersPage.tsx  # Influencer discovery (/influencers)
    │   ├── 📄 InfluencerDashboard.tsx  # Influencer workspace (/influencer) 🔒
    │   ├── 📄 BrandDashboard.tsx       # Brand workspace (/brand) 🔒
    │   └── 📄 AdminDashboard.tsx       # Admin console (/admin) 🔒
    │
    ├── 📁 services/                # API integration layer
    │   └── 📄 api.ts               # Axios client & service methods
    │
    └── 📁 types/                   # TypeScript type definitions
        └── 📄 index.ts             # All interface & type definitions

════════════════════════════════════════════════════════════════════════

KEY FEATURES BY FILE:

✅ main.tsx
   - ReactDOM.createRoot() initialization
   - Strict mode enabled
   - App component rendering

✅ App.tsx
   - React Router setup (8 routes)
   - AuthProvider wrapper
   - Layout with Navbar & Footer
   - Protected routes configuration

✅ index.css
   - Tailwind directives
   - Custom animations
   - Global styling
   - Scrollbar styling

✅ Navbar.tsx
   - Logo & brand
   - Desktop navigation menu
   - Mobile hamburger menu
   - User profile display
   - Auth buttons

✅ Footer.tsx
   - Company information
   - Quick links
   - Copyright info
   - Responsive grid

✅ ProtectedRoute.tsx
   - Role-based access control
   - Authentication check
   - Loading state handling
   - Redirect logic

✅ AuthContext.tsx
   - JWT token management
   - User state persistence
   - useAuth() custom hook
   - Login/logout functions

✅ HomePage.tsx (Public)
   - Hero section
   - Feature cards
   - Statistics display
   - CTA buttons
   - Gradient background

✅ LoginPage.tsx (Public)
   - Email & password form
   - Error handling
   - Loading state
   - Form validation

✅ SignupPage.tsx (Public)
   - Multi-field registration
   - Category selection
   - Bio textarea
   - Password confirmation
   - Influencer onboarding

✅ CampaignsPage.tsx (Public)
   - Campaign grid (3 columns)
   - Status filtering
   - Campaign cards with details
   - Budget & timeline display
   - Call-to-action buttons

✅ InfluencersPage.tsx (Public)
   - Influencer grid (4 columns)
   - Category filtering
   - Influencer profiles
   - Engagement metrics
   - Collaboration buttons

✅ InfluencerDashboard.tsx (Protected - influencer role)
   - Welcome message
   - 4 metric widgets
   - Submission tracking
   - New submission modal
   - Recent activity list

✅ BrandDashboard.tsx (Protected - brand role)
   - Campaign overview
   - 4 KPI cards
   - Budget tracking
   - Create campaign modal
   - Campaign list management

✅ AdminDashboard.tsx (Protected - admin role)
   - Platform statistics
   - Campaign overview
   - Pending submissions queue
   - System health metrics
   - User management ready

✅ api.ts Service Layer
   - Axios instance with interceptors
   - authService (login, logout, register)
   - campaignService (CRUD)
   - influencerService (CRUD)
   - contentSubmissionService (CRUD)
   - approvalService (read, create)
   - metricsService (read, create)

✅ types/index.ts
   - LoginRequest type
   - AuthResponse type
   - User type
   - Campaign type
   - Influencer type
   - ContentSubmission type
   - Approval type
   - PerformanceMetrics type

════════════════════════════════════════════════════════════════════════

ROUTE STRUCTURE:

Public Routes:
  / ................................. Home (landing page)
  /login ............................. Login form
  /signup ............................ Influencer signup form
  /campaigns ......................... Campaign discovery page
  /influencers ....................... Influencer discovery page

Protected Routes (requires auth):
  /influencer ....................... 🔒 Influencer Dashboard (role: influencer)
  /brand ............................ 🔒 Brand Dashboard (role: brand)
  /admin ............................ 🔒 Admin Dashboard (role: admin)

════════════════════════════════════════════════════════════════════════

DESIGN SYSTEM:

Colors:
  Primary: #9333ea - #7e22ce (Purple)
  Success: #10b981 (Green)
  Warning: #f59e0b (Yellow)
  Error: #ef4444 (Red)
  Info: #3b82f6 (Blue)
  Background: #f9fafb (Light Gray)
  Text: #111827 (Dark)

Typography:
  Font Family: Inter (Google Fonts)
  Desktop Heading: 4xl-6xl font-bold
  Section Heading: 2xl-3xl font-bold
  Body: base-lg font-regular
  Small: sm-xs font-medium

Spacing:
  Gutter: px-4 sm:px-6 lg:px-8
  Container: max-w-7xl
  Gap: gap-6 to gap-8
  Padding: p-6 to p-8

Borders:
  Rounded: lg (8px) to 2xl (16px)
  Border Color: border-gray-200/300
  Border Width: 1px

Shadows:
  Card: shadow-sm to shadow-lg
  On Hover: hover:shadow-lg transition

════════════════════════════════════════════════════════════════════════

DEVELOPMENT WORKFLOW:

1. Install & Setup:
   $ cd frontend
   $ npm install

2. Development Server:
   $ npm run dev
   Opens: http://localhost:3000

3. Production Build:
   $ npm run build
   Output: dist/ folder

4. Preview Build:
   $ npm run preview
   Test: http://localhost:4173

════════════════════════════════════════════════════════════════════════

PACKAGE SCRIPTS:

npm run dev .......... Start development server (http://localhost:3000)
npm run build ....... Build for production (minified, optimized)
npm run preview ..... Preview production build locally

════════════════════════════════════════════════════════════════════════

DEPENDENCIES:

Runtime:
  react: ^18.2.0 ............... UI library
  react-dom: ^18.2.0 ........... DOM rendering
  react-router-dom: ^6.20.0 .... Routing
  axios: ^1.6.0 ............... HTTP client
  lucide-react: ^0.292.0 ...... Icon library

Dev:
  vite: ^5.0.8 ................ Build tool
  typescript: ^5.3.3 .......... Type checking
  tailwindcss: ^3.4.0 ......... Styling
  @vitejs/plugin-react-swc .... Fast React transform
  postcss: ^8.4.32 ............ CSS processor
  autoprefixer: ^10.4.16 ...... CSS vendor prefixes

════════════════════════════════════════════════════════════════════════

AUTHENTICATION FLOW:

1. User visits /login or /signup
2. Form submission calls authService
3. Backend returns JWT token + user data
4. Token saved to localStorage
5. User data saved to AuthContext
6. Axios interceptor adds token to all requests
7. Protected routes check AuthContext
8. Logout clears token & context
9. Redirect to /login on token expiry

════════════════════════════════════════════════════════════════════════

API INTEGRATION:

Frontend: http://localhost:3000
Backend:  http://localhost:9000/api

All requests:
- Include JWT token in Authorization header
- Use JSON content-type
- Handle 401/403 responses with logout
- Show error messages to user
- Manage loading states

════════════════════════════════════════════════════════════════════════

RESPONSIVE DESIGN:

Mobile (< 640px):
  - Single column layouts
  - Hamburger menu
  - Stack form fields
  - Touch-friendly buttons

Tablet (640px - 1024px):
  - 2 column grids
  - Optimized spacing
  - Readable typography
  - Balanced layout

Desktop (> 1024px):
  - 3-4 column grids
  - Full navigation bar
  - Large hero sections
  - Optimized whitespace

════════════════════════════════════════════════════════════════════════

CUSTOM ANIMATIONS:

slideDown: Navbar dropdown animation (0.3s ease-out)
fadeIn: Page transitions (0.3s ease-out)

════════════════════════════════════════════════════════════════════════

BUILT WITH ❤️

React 18 + TypeScript + Tailwind CSS + Vite

Production-ready, fully typed, responsive, accessible, elegant.
