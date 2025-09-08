import { Link } from "react-router-dom";
import { useFormData } from "../hooks/forms";

interface IForm {
    fullname: string;
    email: string;
    password: string;
}


export const Registration = () => {
    const regForm: IForm = {
        fullname: "",
        email: "",
        password: ""
    }

    const [formData, handleChange, setFormData] = useFormData(regForm);

    

    return (
        <>
            <div className="bg-neutral-950 w-[100vw] h-[100vh] flex items-center justify-center">
                <div className='bg-neutral-700 text-white min-w-[48vw] min-h-[45vh] flex flex-col justify-center space-y-14 items-center border-emerald-500 border-4 rounded-[2rem]'>
                    <span className='text-4xl font-bold text-emerald-200'>Регистрация</span>

                    <div className="">
                        <form className=" flex flex-col space-y-6 w-[30vw] text-[1.7rem]" action="">

                            <div className="flex flex-row justify-between">ФИО <input className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition" type="text" name="fullname" value={formData.fullname} onChange={handleChange}/></div>

                            <div className="flex flex-row justify-between">e-mail <input className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"  type="email" name="email" value={formData.email} onChange={handleChange}/></div>

                            <div className="flex flex-row justify-between">Пароль <input className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"  type="password" name="password" value={formData.password} onChange={handleChange}/></div>

                        </form>
                    </div>
                        <div className="flex flex-col items-center space-y-2">
                            <button className="bg-emerald-600 pt-3 px-5 pb-4 rounded-3xl align-middle text-2xl transition-transform duration-300  hover:scale-110 active:bg-emerald-400 active:scale-105 ">Зарегистрироваться</button>
                            <Link to="/login">Есть аккаунт?</Link>
                        </div>
                </div>
            </div>
        </>
    )
}
