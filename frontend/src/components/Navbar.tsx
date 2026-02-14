import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { LogOut, Menu, X } from 'lucide-react';
import { useState } from 'react';

export function Navbar() {
  const { user, logout, isAuthenticated } = useAuth();
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  return (
    <nav className="bg-white border-b border-gray-200 sticky top-0 z-50 shadow-sm">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between items-center h-16">
          <Link to="/" className="flex items-center space-x-2">
            <div className="w-8 h-8 bg-gradient-to-br from-purple-600 to-purple-500 rounded-lg flex items-center justify-center">
              <span className="text-white font-bold text-lg">B</span>
            </div>
            <span className="text-xl font-bold text-gray-900">BeautySquad</span>
          </Link>

          <div className="hidden md:flex items-center space-x-8">
            <Link to="/campaigns" className="text-gray-600 hover:text-purple-600 transition">
              Campaigns
            </Link>
            <Link to="/influencers" className="text-gray-600 hover:text-purple-600 transition">
              Influencers
            </Link>
            {isAuthenticated && user?.role === 'admin' && (
              <Link to="/admin" className="text-gray-600 hover:text-purple-600 transition">
                Admin
              </Link>
            )}
          </div>

          <div className="hidden md:flex items-center space-x-4">
            {isAuthenticated ? (
              <>
                <div className="text-sm">
                  <p className="text-gray-700 font-medium">{user?.firstName} {user?.lastName}</p>
                  <p className="text-gray-500">{user?.role}</p>
                </div>
                <button
                  onClick={() => {
                    logout();
                  }}
                  className="p-2 text-gray-600 hover:bg-gray-100 rounded-lg transition"
                  title="Logout"
                >
                  <LogOut size={20} />
                </button>
              </>
            ) : (
              <>
                <Link
                  to="/login"
                  className="px-4 py-2 text-gray-700 hover:text-purple-600 font-medium transition"
                >
                  Login
                </Link>
                <Link
                  to="/signup"
                  className="px-4 py-2 bg-purple-600 text-white rounded-lg hover:bg-purple-700 transition font-medium"
                >
                  Sign Up
                </Link>
              </>
            )}
          </div>

          <button
            className="md:hidden p-2"
            onClick={() => setIsMenuOpen(!isMenuOpen)}
          >
            {isMenuOpen ? <X size={24} /> : <Menu size={24} />}
          </button>
        </div>

        {isMenuOpen && (
          <div className="md:hidden pb-4 border-t border-gray-200 mt-4">
            <Link to="/campaigns" className="block py-2 text-gray-600 hover:text-purple-600">
              Campaigns
            </Link>
            <Link to="/influencers" className="block py-2 text-gray-600 hover:text-purple-600">
              Influencers
            </Link>
            {!isAuthenticated ? (
              <>
                <Link to="/login" className="block py-2 text-gray-600 hover:text-purple-600">
                  Login
                </Link>
                <Link to="/signup" className="block py-2 text-purple-600 font-medium">
                  Sign Up
                </Link>
              </>
            ) : (
              <button onClick={logout} className="w-full text-left py-2 text-gray-600 hover:text-purple-600">
                Logout
              </button>
            )}
          </div>
        )}
      </div>
    </nav>
  );
}
