import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        //target: 'https://clashofclans-1-bwjm.onrender.com',
        target: 'https://localhost:7016',
        changeOrigin: true,
        secure: false,
      },
    },
  },
})
