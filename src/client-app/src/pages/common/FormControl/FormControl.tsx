import React, {FC} from 'react';

// import style from './FormControl.module.css';

interface FormControlProps {
  errorText: string
}

const FormControl: FC<FormControlProps> = (props) => {
    return (
        <div>
            {props.errorText}
        </div>
    )
}

export default FormControl;