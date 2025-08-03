interface FormFieldProps {
  id: string;
  label?: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  placeholder?: string;
  error?: string;
  type?: string;
}

const FormField = ({
  id,
  label,
  value,
  onChange,
  placeholder,
  error,
  type = 'text',
}: FormFieldProps) => (
  <div>
    {label && (
      <label
        htmlFor={id}
        className='block text-sm font-medium text-gray-700 mb-1'
      >
        {label}
      </label>
    )}
    <input
      type={type}
      id={id}
      name={id}
      value={value}
      onChange={onChange}
      placeholder={placeholder}
      className='form-input'
    />
    {error && <p className='text-red-500 text-sm mt-1'>{error}</p>}
  </div>
);

export default FormField;
