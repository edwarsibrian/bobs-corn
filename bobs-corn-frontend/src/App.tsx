import { BrowserRouter } from "react-router"
import Menu from "./components/Menu"
import AppRoutes from "./AppRoutes"
import './App.css'


function App() {
  

  return (
      <>
          <BrowserRouter>
              <Menu />
              <main className="container py-4">
                  <AppRoutes />
              </main>
          </BrowserRouter>
    </>
  )
}

export default App
