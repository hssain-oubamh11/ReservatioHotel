import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  css: {
    postcss: { plugins: [] }
  },
  server: {
    port: 3000,
    proxy: {
      '/api': {
        target: 'https://localhost:7015',
        changeOrigin: true,
        secure: false,
      }
    }
  }
})
