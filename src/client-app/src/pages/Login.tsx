import { Link } from "react-router-dom";
import { useFormData } from "../hooks/forms";

interface ILognForm {
    login: string;
    password: string;
}


export const Login = () => {
    const loginForm: ILognForm = {
        login: "",
        password: ""
    }

    const [formData, handleChange, setFormData] = useFormData(loginForm);
    return (
        <>
            <div className="bg-neutral-950 w-[100vw] h-[100vh] flex items-center justify-center">
                <div className="bg-neutral-700 text-white min-w-[48vw] min-h-[42vh] flex flex-col justify-center space-y-14 items-center border-emerald-500 border-4 rounded-[2rem]">
                    <span className='text-4xl font-bold text-emerald-200'>Вход</span>

                    <div className="">
                        <form className=" flex flex-col space-y-6 w-[30vw] text-[1.7rem]" action="">

                            <div className="flex flex-row justify-between">Логин <input className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition" type="text" name="login" value={formData.login} onChange={handleChange}/></div>

                            <div className="flex flex-row justify-between">Пароль <input className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"  type="password" name="password" value={formData.password} onChange={handleChange}/></div>

                        </form>
                    </div>
                    <div className="flex flex-row items-center space-x-3">
                        <button className="bg-emerald-600 pt-3 px-8 pb-4 rounded-3xl align-middle text-2xl transition-transform duration-300  hover:scale-110 active:bg-emerald-400 active:scale-105" onClick={() => window.location.replace("/")}>Войти</button>
                        <Link to="/registration">Создать аккаунт?</Link>
                    </div>
                </div>
            </div>
        </>
    )
}

