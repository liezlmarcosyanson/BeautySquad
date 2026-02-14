# BeautySquad Frontend - Quick Start Guide

## 📋 Project Overview

A modern, minimal and elegant client-facing frontend for the BeautySquad influencer campaign management platform. Built with React, TypeScript, Tailwind CSS, and Vite.

## 🎨 Design Philosophy

- **Minimal & Elegant**: Clean interface with purple gradient accent colors
- **Responsive**: Works seamlessly on desktop, tablet, and mobile
- **Fast**: Optimized with Vite and React 18
- **Accessible**: WCAG compliant with semantic HTML
- **Tailwind CSS**: Utility-first styling approach

## 📁 Directory Structure

```
frontend/
├── src/
│   ├── components/          # Reusable UI components
│   │   ├── Navbar.tsx      # Navigation header
│   │   ├── Footer.tsx      # Footer component
│   │   └── ProtectedRoute.tsx # Route protection
│   ├── context/            # React Context providers
│   │   └── AuthContext.tsx # Authentication state
│   ├── pages/              # Page components
│   │   ├── HomePage.tsx           # Landing page
│   │   ├── LoginPage.tsx          # Login form
│   │   ├── SignupPage.tsx         # Influencer signup
│   │   ├── CampaignsPage.tsx      # Campaign listing
│   │   ├── InfluencersPage.tsx    # Influencer discovery
│   │   ├── InfluencerDashboard.tsx # Influencer workspace
│   │   ├── BrandDashboard.tsx      # Brand workspace
│   │   └── AdminDashboard.tsx      # Admin console
│   ├── services/           # API integration
│   │   └── api.ts         # Axios client & services
│   ├── types/             # TypeScript interfaces
│   │   └── index.ts       # Type definitions
│   ├── App.tsx            # Main app component
│   ├── main.tsx           # Entry point
│   └── index.css          # Global styles
├── index.html             # HTML template
├── vite.config.ts         # Vite configuration
├── tailwind.config.js     # Tailwind CSS config
├── postcss.config.js      # PostCSS config
├── tsconfig.json          # TypeScript config
├── package.json           # Dependencies & scripts
└── .env.example           # Environment variables template
```

## 🚀 Getting Started

### Prerequisites
- Node.js 16+ and npm

### Installation

```bash
cd frontend
npm install
```

### Development Server

```bash
npm run dev
```

This will start the dev server at **http://localhost:3000** and automatically open it in your browser.

### Build for Production

```bash
npm run build
```

### Preview Production Build

```bash
npm run preview
```

## 🔐 Authentication

The app uses JWT token-based authentication:

1. **Login**: Existing users sign in via email/password
2. **Signup**: New influencers create an account
3. **Protected Routes**: Dashboards require authentication and role-based access

Tokens are stored in `localStorage` and automatically included in API requests.

## 📄 Pages & Features

### 🏠 Home Page (`/`)
- Landing page with hero section
- Feature highlights
- Platform statistics
- CTA buttons to sign in/up

### 🔑 Login Page (`/login`)
- Email & password form
- Error handling
- Link to signup page

### 📝 Signup Page (`/signup`)
- Multi-field influencer registration form
- Bio, category, follower count
- Password validation
- Terms acceptance

### 📢 Campaigns Page (`/campaigns`)
- Filterable campaign listings
- Budget, timeline, and status display
- Card-based grid layout
- Status badges (active, pending, completed)

### 👥 Influencers Page (`/influencers`)
- Categoryfiltering
- Influencer cards with stats
- Engagement rate display
- "Collaborate" action button

### 📊 Influencer Dashboard (`/influencer`)
- Protected route (requires influencer role)
- Submission statistics
- Recent submissions list
- New submission modal
- Performance metrics

### 🎯 Brand Dashboard (`/brand`)
- Protected route (requires brand role)
- Campaign overview
- Active campaign count
- Budget tracking  
- Create campaign modal
- Campaign list with actions

### ⚙️ Admin Dashboard (`/admin`)
- Protected route (requires admin role)
- System-wide statistics
- Campaign overview
- Pending submissions queue
- User management interface

## 🔌 API Integration

The frontend connects to the backend API at `http://localhost:9000/api`

### Services Available:
- `authService` - Login, logout, register
- `campaignService` - CRUD operations on campaigns
- `influencerService` - Influencer management
- `contentSubmissionService` - Submission workflows
- `approvalService` - Approval workflows
- `metricsService` - Performance metrics

All services use Axios with automatic JWT token injection.

## 🎯 Features

✅ User Authentication (Login/Signup)
✅ Role-based Access Control (Influencer/Brand/Admin)
✅ Campaign Discovery & Browsing
✅ Influencer Discovery & Filtering
✅ Influencer Content Submissions
✅ Brand Campaign Management
✅ Admin Platform Oversight
✅ Real-time Metrics & Analytics
✅ Responsive Mobile Design
✅ Elegant Dark Theme with Purple Accents

## 🎨 Color Scheme

- **Primary**: Purple (`#9333ea` - `#7e22ce`)
- **Background**: Light Gray (`#f9fafb`)
- **Text**: Dark Gray (`#111827`)
- **Accents**: Green (success), Yellow (warning), Red (error), Blue (info)

## 📱 Responsive Design

- Mobile-first approach
- Breakpoints: `sm` (640px), `md` (768px), `lg` (1024px), `xl` (1280px)
- Touch-friendly controls
- Optimized typography

## 🔄 State Management

- **Auth Context**: Global auth state and user info
- **React Router**: Navigation and protected routes
- **Local Storage**: Token persistence
- **Axios Interceptors**: Automatic token injection

## 🛠️ Development Tips

### Adding a New Page

1. Create component in `src/pages/ComponentName.tsx`
2. Add route in `src/App.tsx`
3. Import and configure in routes array

### Adding API Service

1. Extend `src/services/api.ts`
2. Create new service object with methods
3. Use in components via imports

### Styling

- Use Tailwind classes for styling
- Custom CSS in `src/index.css` for animations
- Dark mode compatible (future enhancement)

## 📦 Dependencies

### Production
- `react` ^18.2.0 - UI library
- `react-dom` ^18.2.0 - DOM rendering
- `react-router-dom` ^6.20.0 - Client-side routing
- `axios` ^1.6.0 - HTTP client
- `lucide-react` ^0.292.0 - Icon library

### Development
- `vite` ^5.0.8 - Build tool
- `typescript` ^5.3.3 - Type safety
- `tailwindcss` ^3.4.0 - Styling
- `@vitejs/plugin-react-swc` ^3.5.0 - Fast React transform

## 🐛 Troubleshooting

### API Connection Issues
- Ensure backend is running on `http://localhost:9000`
- Check CORS configuration in backend
- Verify network tab in DevTools

### Port Already in Use
```bash
# Kill process on port 3000 (macOS/Linux)
lsof -i :3000 | grep LISTEN | awk '{print $2}' | xargs kill -9

# Or change port in vite.config.ts
```

### Build Errors
```bash
# Clear node_modules and reinstall
rm -rf node_modules package-lock.json
npm install
```

## 📞 Support

For issues or questions, check:
1. Backend API logs
2. Browser DevTools console/network
3. Component propTypes and TypeScript errors

## 🚀 Deployment

The frontend can be deployed to:
- **Vercel**: Connected GitHub repo auto-deploys
- **Netlify**: Drag & drop build or git integration
- **GitHub Pages**: Run `npm run build`, push `dist/` folder
- **Self-hosted**: Docker or any static host

Update `VITE_API_BASE_URL` in `.env` for production API endpoint.

---

**Built with ❤️ using React, TypeScript, and Tailwind CSS**
