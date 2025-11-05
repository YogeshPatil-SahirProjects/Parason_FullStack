import React from 'react';
import { Mail, Phone, MapPin, ArrowRight } from 'lucide-react';

const About: React.FC = () => {
    return (
        <div className="min-h-screen bg-gray-50">
            <div className="container mx-auto px-4 py-12 max-w-5xl">
                {/* Header */}
                <div className="text-center mb-12">
                    <h1 className="text-4xl font-bold text-gray-900 mb-4">About Us</h1>
                    <div className="w-24 h-1 bg-blue-600 mx-auto"></div>
                </div>

                {/* Who We Are */}
                <section className="bg-white rounded-lg shadow-md p-8 mb-8">
                    <h2 className="text-2xl font-semibold text-gray-900 mb-4">Who We Are</h2>
                    <p className="text-gray-700 leading-relaxed mb-4">
                        Parason CPQ is a leading provider of Configure-Price-Quote solutions designed specifically
                        for the manufacturing industry. We empower businesses to streamline their sales processes,
                        eliminate pricing errors, and deliver accurate quotes in minutes, not hours.
                    </p>
                    <p className="text-gray-700 leading-relaxed">
                        Our team combines deep industry expertise with cutting-edge technology to deliver
                        solutions that transform how manufacturers engage with their customers and close deals.
                    </p>
                </section>

                {/* What We Do */}
                <section className="bg-white rounded-lg shadow-md p-8 mb-8">
                    <h2 className="text-2xl font-semibold text-gray-900 mb-4">What We Do</h2>
                    <p className="text-gray-700 leading-relaxed mb-6">
                        We provide comprehensive CPQ solutions that address the unique challenges of complex
                        manufacturing environments:
                    </p>
                    <div className="grid md:grid-cols-2 gap-6">
                        <div className="flex items-start space-x-3">
                            <div className="w-2 h-2 bg-blue-600 rounded-full mt-2"></div>
                            <div>
                                <h3 className="font-semibold text-gray-900 mb-1">Intelligent Configuration</h3>
                                <p className="text-gray-600 text-sm">
                                    Guide users through complex product configurations with rule-based logic
                                    that ensures only valid combinations.
                                </p>
                            </div>
                        </div>
                        <div className="flex items-start space-x-3">
                            <div className="w-2 h-2 bg-blue-600 rounded-full mt-2"></div>
                            <div>
                                <h3 className="font-semibold text-gray-900 mb-1">Dynamic Pricing Engine</h3>
                                <p className="text-gray-600 text-sm">
                                    Automate pricing calculations with volume discounts, customer-specific pricing,
                                    and real-time cost adjustments.
                                </p>
                            </div>
                        </div>
                        <div className="flex items-start space-x-3">
                            <div className="w-2 h-2 bg-blue-600 rounded-full mt-2"></div>
                            <div>
                                <h3 className="font-semibold text-gray-900 mb-1">Professional Quote Generation</h3>
                                <p className="text-gray-600 text-sm">
                                    Create polished, branded quotes and proposals that win business and
                                    accelerate your sales cycle.
                                </p>
                            </div>
                        </div>
                        <div className="flex items-start space-x-3">
                            <div className="w-2 h-2 bg-blue-600 rounded-full mt-2"></div>
                            <div>
                                <h3 className="font-semibold text-gray-900 mb-1">Seamless Integration</h3>
                                <p className="text-gray-600 text-sm">
                                    Connect with your existing ERP, CRM, and manufacturing systems to maintain
                                    a single source of truth.
                                </p>
                            </div>
                        </div>
                    </div>
                </section>

                {/* What Makes Us Different */}
                <section className="bg-white rounded-lg shadow-md p-8 mb-8">
                    <h2 className="text-2xl font-semibold text-gray-900 mb-4">What Makes Us Different</h2>
                    <div className="space-y-4">
                        <div className="border-l-4 border-blue-600 pl-4">
                            <h3 className="font-semibold text-gray-900 mb-2">Industry Expertise</h3>
                            <p className="text-gray-700">
                                We understand manufacturing complexity because we've worked exclusively in this
                                space, building solutions that address real-world challenges.
                            </p>
                        </div>
                        <div className="border-l-4 border-blue-600 pl-4">
                            <h3 className="font-semibold text-gray-900 mb-2">Rapid Implementation</h3>
                            <p className="text-gray-700">
                                Our proven methodology and pre-built templates mean you can go live in weeks,
                                not months, with minimal disruption to your operations.
                            </p>
                        </div>
                        <div className="border-l-4 border-blue-600 pl-4">
                            <h3 className="font-semibold text-gray-900 mb-2">Scalable Architecture</h3>
                            <p className="text-gray-700">
                                From small distributors to global OEMs, our platform scales with your business
                                and adapts to your evolving needs.
                            </p>
                        </div>
                        <div className="border-l-4 border-blue-600 pl-4">
                            <h3 className="font-semibold text-gray-900 mb-2">Customer-Centric Support</h3>
                            <p className="text-gray-700">
                                Our dedicated support team ensures your success with ongoing training,
                                optimization, and responsive technical assistance.
                            </p>
                        </div>
                    </div>
                </section>

                {/* Mission & Vision */}
                <section className="bg-gradient-to-r from-blue-500 to-indigo-600 rounded-lg shadow-md p-8 mb-8 text-white">
                    <div className="grid md:grid-cols-2 gap-8">
                        <div>
                            <h2 className="text-2xl font-semibold mb-4">Our Mission</h2>
                            <p className="leading-relaxed">
                                To empower manufacturing businesses with intelligent CPQ solutions that eliminate
                                complexity, reduce errors, and accelerate revenue growth.
                            </p>
                        </div>
                        <div>
                            <h2 className="text-2xl font-semibold mb-4">Our Vision</h2>
                            <p className="leading-relaxed">
                                To be the trusted CPQ partner for manufacturers worldwide, transforming how they
                                configure, price, and sell complex products.
                            </p>
                        </div>
                    </div>
                </section>

                {/* Call to Action */}
                <section className="bg-white rounded-lg shadow-md p-8 text-center">
                    <h2 className="text-2xl font-semibold text-gray-900 mb-4">Get in Touch</h2>
                    <p className="text-gray-700 mb-6 max-w-2xl mx-auto">
                        Ready to transform your quoting process? We'd love to discuss how Parason CPQ
                        can help your business grow.
                    </p>
                    <div className="flex flex-col md:flex-row items-center justify-center gap-6 mb-8">
                        <a
                            href="mailto:info@parasoncpq.com"
                            className="flex items-center space-x-2 text-gray-700 hover:text-blue-600 transition-colors"
                        >
                            <Mail className="w-5 h-5" />
                            <span>info@parasoncpq.com</span>
                        </a>
                        <a
                            href="tel:+1234567890"
                            className="flex items-center space-x-2 text-gray-700 hover:text-blue-600 transition-colors"
                        >
                            <Phone className="w-5 h-5" />
                            <span>+1 (234) 567-890</span>
                        </a>
                        <div className="flex items-center space-x-2 text-gray-700">
                            <MapPin className="w-5 h-5" />
                            <span>Contact us for location details</span>
                        </div>
                    </div>
                    <button className="bg-blue-600 hover:bg-blue-700 text-white font-semibold px-8 py-3 rounded-lg transition-colors inline-flex items-center space-x-2 shadow-md">
                        <span>Schedule a Demo</span>
                        <ArrowRight className="w-5 h-5" />
                    </button>
                </section>

                {/* Note for customization */}
                <div className="mt-8 p-4 bg-blue-50 border border-blue-200 rounded-lg">
                    <p className="text-sm text-gray-700">
                        <span className="font-semibold">Note:</span> This content is based on the Parason CPQ branding
                        found in your application. You can customize all company details, contact information,
                        and content to match your specific requirements.
                    </p>
                </div>
            </div>
        </div>
    );
};

export default About;
