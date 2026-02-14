{
  "name": "BeautySquad Frontend",
  "short_description": "Modern influencer campaign management platform frontend",
  "description": "A minimal and elegant React frontend for BeautySquad - connecting brands with influencers.",
  "features": [
    "User authentication (Login/Signup)",
    "Campaign listing and discovery",
    "Influencer discovery and filtering",
    "Influencer dashboard for submissions",
    "Brand dashboard for campaign management",
    "Admin dashboard for platform oversight",
    "Real-time metrics and analytics",
    "Responsive design with Tailwind CSS",
    "Role-based access control"
  ],
  "tech_stack": {
    "frontend_framework": "React 18",
    "language": "TypeScript",
    "styling": "Tailwind CSS",
    "routing": "React Router v6",
    "http_client": "Axios",
    "build_tool": "Vite",
    "ui_icons": "Lucide React"
  },
  "pages": {
    "home": "/",
    "login": "/login",
    "signup": "/signup",
    "campaigns": "/campaigns",
    "influencers": "/influencers",
    "influencer_dashboard": "/influencer (protected)",
    "brand_dashboard": "/brand (protected)",
    "admin_dashboard": "/admin (protected)"
  },
  "api_endpoint": "http://localhost:9000/api",
  "ports": {
    "dev_server": 3000
  },
  "installation": "npm install",
  "dev_command": "npm run dev",
  "build_command": "npm run build",
  "preview_command": "npm run preview"
}
