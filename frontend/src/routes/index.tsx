import { createBrowserRouter, RouterProvider } from "react-router"
import MainLayout from "../layouts/MainLayout"
import AuthLayout from "../layouts/AuthLayout"
import Home from "../pages/Home"
import About from "../pages/About"
import Contact from "../pages/Contact"
import Login  from "../pages/Login"
import Register from "../pages/Register"
import Forgotpassword from "../pages/Forgotpassword"

const router = createBrowserRouter([
    {
        path: "/",
        element: <MainLayout />,
        children: [
            {
                index: true,
                element: <Home />
            },
            {
                path: "about",
                element: <About />
            },
            {
                path: "contact",
                element: <Contact />
            }
        ]
    },

        {
        path: "auth",
        element: <AuthLayout />,
        children: [
            {
                path:"login",
                element: <Login />
            },
            {
                path: "register",
                element: <Register/>
            },
            {
                path: "Forgot-password",
                element: <Forgotpassword />
            }
        ]
    }
])

export const AppRouter = () => {
  return (
    <RouterProvider router={router} />
  )
}